//2018/01/02 New add Support QQ Browser
//2018/01/04 New add mute & unmute fix contorl for playrate seek
//2018/01/05 Remove mute & unmute control , remove init muted, please use muted in video tag
//2018/01/05 New add lastSoundState to save last muted state, and default Muted
//2018/01/05 Fixed isVisibilityChange
//2018/01/08 Diabled subtitles & default max buffer set 50MB
//2018/01/08 Mandatory use UID
//2018/01/08 Make some adjustment to ChkBuf
//2018/01/09 Customize disableSeek maxReadyChk maxBufferedChk useNativePlayer
//2018/01/11 Fixed hls.js config
//2018/01/12 Adjust the seek setting, when continuously 2 times over maxSeekSec to execute seek
//2018/01/12 Adjust the setting, when delay < 0.5 to pause stream to avoid SEEK_OVER_HOLE
//2018/01/14 Fixed onplay repeat additional
//2018/01/14 Adjust hls.js play workthrough, Fixed hls.js init fail restart --> Uncaught (in promise) DOMException: The play() request was interrupted by a call to pause().
//2018/01/14 Adjust Buffered < 0.5 resume need buffered 4 sec
//2018/01/15 Fixed Uncaught DOMException: Failed to execute 'end' on 'TimeRanges'
//2018/01/15 Adjust disable Seek only control playrate
//2018/01/16 New add maxCurrentChk, maxBufferOverChk to check currentTime and buffered over maxSeekSec
//2018/01/16 catch iOS safari buffered.end() index error
//2018/01/23 Fixed can't clearInterval bug
//2018/01/26 New add iOS seek(video.seekable.end)
//2018/02/22 New add hls.js Support EXT-X-PROGRAM-DATE-TIME
//2018/02/23 New add iOS m3u8 parse and use request m3u8 to get EXT-X-PROGRAM-DATE-TIME
//2018/02/23 New add iOS and hls.js Client timecode
//2018/02/26 Adjust iOS and hls.js timecode operation (iOS use XMLHttpRequest to get last fragment as offset, hls.js use data.frag.duration as offset)
//2018/03/05 iOS support Use Simple-RTMP-Server m3u8 header Last-Modified to operation timecode
//2018/03/15 Fixed WeChat and QQBrowser Playback
//2018/03/19 Adjust mse support statement
//2018/04/20 New Add Support Both manifest & fragment EXT-X-PROGRAM-DATE-TIME
//2018/04/24 Fixed iOS get buffered.end error
//2018/04/25 Fixed iOS initial buffer not enough paused, but when buffer is enough can't resume playback
//2018/06/19 Fixed iSO EXT-X-PROGRAM-DATE-TIME to DateTime & TimeStamp Error
//2018/07/09 Remove streamA
//2018/07/10 New Add check Griffinmas no stream loop
//2018/07/24 Fixed When Fragment = 1 Check Griffinmas no stream loop times Error
//2020/01/16 Add video poster message
var HLSJSAPI = {
    Version: '0.8.7c',
    ReleaseDate:'2018/07/24',
    Finish: ''
};

HLSJSAPI.Class = function(){

    console.log('Version:[' + HLSJSAPI.Version +'] Release Date:[' + HLSJSAPI.ReleaseDate +']');

    var HLSObj = function( option ) {
        var _this = this;
        this.option = option || {};
        this.Player = null;
        this.vendorHidden;
        this.vendorVisibilityChange;
        this.url = null;
        this.hls = null;
        this.doSeek;
        this.playState = null;
        this.ready = null;
        this.network = null;
        this.bufferTime = 0, this.currentTime = 0, this.delayTime = 0, this.seekTime = 0, this.times = 0, this.lastTime = 0, this.seekableEnd = 0, this.lastBuffer = 0, this.lastCurrent = 0, this.readyTimes = 0,  this.bufferTimes = 0, this.currentStopTimes = 0;
        this.maniLoading = 0, this.maniLoaded = 0, this.maniInitPTS = 0, this.maniPaesed = 0, this.fragParsingInit = 0;
        this.errorcode = '';
        this.timecode = null, this.timeStamp = null, this.lastFragInterval = null, this.m3u8modify = null;
        this.iOS = !!navigator.platform && /iPad|iPhone|iPod/.test(navigator.platform);
        this.userAgent = navigator.userAgent;
        this.streamaSwitch = true;
        this.lastSoundState = null;
        this.hlsHidden = false;
        this.manipause = false;
        this.inSeek = false;
        this.m3u8get = null, this.m3u8Url = null;
        this.lastbasechk = '', this.basechk = '', this.basechktimes = 0, this.lastclientTime = 0;
        this.errDetails = '', this.updateTimes = 0;

        //Init Setting Varable
        //Disable Seek --> Default: false
        this.disableSeek = typeof this.option.disableSeek != 'undefined' ? this.option.disableSeek : false;
        //readyState Check Max Count --> Default: 10
        this.maxReadyChk = typeof this.option.maxReadyChk != 'undefined' ? this.option.maxReadyChk : 10;
        //Buffered Check Max Count --> Default: 10
        this.maxBufferedChk = typeof this.option.maxBufferedChk != 'undefined' ? this.option.maxBufferedChk : 10;
        //ChkSeek Max Sec --> Default: 8
        this.maxSeekSec = typeof this.option.maxSeekSec != 'undefined' ? this.option.maxSeekSec : 8;
        //Buffered Over maxSeekSec  max times set (disableSeek:true) --> Default: 2
        this.maxBufferOverChk = typeof this.option.maxBufferOverChk  != 'undefined' ? this.option.maxBufferOverChk : 2;
        //CurrentTime no change check times--> Default: 2
        this.maxCurrentChk = typeof this.option.maxCurrentChk  != 'undefined' ? this.option.maxCurrentChk : 2;
        //Use Natvie Player Control Bar to Control Audio --> Default: false
        this.useNativeVolume = typeof this.option.useNativeVolume != 'undefined' ? this.option.useNativeVolume : false;
        //Default Muted --> Default: true
        this.defaultMuted = typeof this.option.defaultMuted != 'undefined' ? this.option.defaultMuted : true;
        //Use Native Player --> Default: false
        this.useNativePlayer = typeof this.option.useNativePlayer != 'undefined' ? this.option.useNativePlayer : false;
        //Use ProgramDateTime (EXT-X-PROGRAM-DATE-TIME) --> Default: false
        this.useProgramDateTime = typeof this.option.useProgramDateTime != 'undefined' ? this.option.useProgramDateTime : false;
        //Use TimeCode (timeStamp - ( _this.Player.buffered.end(_this.Player.buffered.length - 1 ) - _this.Player.currentTime ) * 1000) --> Default: false
        this.useTimeCode = typeof this.option.useTimeCode != 'undefined' ? this.option.useTimeCode : false;
        //Use ProgramDateTime (EXT-X-PROGRAM-DATE-TIME) Type (manifest or fragment) --> Default:manifest 
        this.ProgramDateTimeType = typeof this.option.ProgramDateTimeType != 'undefined' ? this.option.ProgramDateTimeType : 'manifest';

        //Default use timecode when useTimeCode and ProgramDateTime are true
        if ( this.useTimeCode === true ) {
            this.useProgramDateTime = false;
        }

        //Check Browser whether support MSE
        function isSupported() {
            if (typeof window !== 'undefined') {
                var mediaSource = window.MediaSource || window.WebKitMediaSource;
            } else {
                var mediaSource;
            }

            var sourceBuffer = window.SourceBuffer || window.WebKitSourceBuffer;
            var isTypeSupported = mediaSource && typeof mediaSource.isTypeSupported === 'function' && mediaSource.isTypeSupported('video/mp4; codecs="avc1.42E01E,mp4a.40.2"');
            var sourceBufferValidAPI = !sourceBuffer || sourceBuffer.prototype && typeof sourceBuffer.prototype.appendBuffer === 'function' && typeof sourceBuffer.prototype.remove === 'function';
            return !!isTypeSupported && !!sourceBufferValidAPI;
        }

        this.mseSupport = isSupported();

        if ( this.mseSupport === true ) {
            this.hlsSupport = true;
        } else {
            this.hlsSupport = false;
            if ( this.userAgent.indexOf("Android") > -1 ) {  
                this.useProgramDateTime = false;
                this.useTimeCode === false;
            }
        }

        if ( this.useNativePlayer === true ) {
            this.hlsSupport = false;
        }

        //hls.js seek delay 2 sec
        if ( this.hlsSupport === true && this.disableSeek === false ) {
            this.maxSeekSec += 2;
        }

        this.head = document.getElementsByTagName('head')[0];

        this.varObj = new Object();
        //hls.js Max Buffer Length
        this.varObj.maxMaxBufferLength = 500;
        //Max buffer size
        this.varObj.maxBufferSize = 50*1000*1000;
        //Max number of load retries.
        this.varObj.fragLoadingMaxRetry = 1;
        this.varObj.manifestLoadingMaxRetry = 1;
        this.varObj.levelLoadingMaxRetry = 0;
        this.varObj.liveSyncDurationCount = 3;
        this.varObj.liveMaxLatencyDurationCount = 4;
        //number of segments needed to start a playback of Live stream.
        this.varObj.initialLiveManifestSize = 1;
        //Whether or not to force having a key frame in the first AVC sample after a discontinuity
        this.varObj.forceKeyFrameOnDiscontinuity = true;
        //Webvtt subtitles
        this.varObj.enableWebVTT = false;
        //CEA708 subtitles
        this.varObj.enableCEA708Captions = false;

        function isVisibilityChangeSupported() {
            var supported = true;
            if (typeof document.hidden !== "undefined") { // Opera 12.10 and Firefox 18 and later support
                this.vendorHidden = "hidden";
                this.vendorVisibilityChange = "visibilitychange";
            } else if (typeof document.msHidden !== "undefined") {
                this.vendorHidden = "msHidden";
                this.vendorVisibilityChange = "msvisibilitychange";
            } else if (typeof document.webkitHidden !== "undefined") {
                this.vendorHidden = "webkitHidden";
                this.vendorVisibilityChange = "webkitvisibilitychange";
            } else {
                supported = false;
            }
            return supported;
        }

        HLSObj.prototype.HttpClient = function() {
            this.get = function(sUrl, aCallback) {
                var anHttpRequest = new XMLHttpRequest();
                anHttpRequest.onreadystatechange = function() {
                    if (anHttpRequest.readyState == 4 && anHttpRequest.status == 200) {
                        //Simple-RTMP-Server m3u8 header has Last-Modified:
                        var lastmodified = anHttpRequest.getResponseHeader('Last-Modified');
                        if ( lastmodified != null ) {
                            _this.m3u8modify = new Date(lastmodified).getTime();
                        } else {
                            _this.m3u8modify = null;
                        }
                        aCallback(anHttpRequest.responseText);
                    }
                }
                anHttpRequest.open( "GET", sUrl, true );
                anHttpRequest.send( null );
            }
        }

        HLSObj.prototype.m3u8Parse = function( response ) {
            this.m3u8tag = response.split("#");
            this.programDateTime;
            for ( var o in this.m3u8tag) {
                //Get Last EXT-X-PROGRAM-DATE-TIME
                if( this.m3u8tag[o].indexOf("EXT-X-PROGRAM-DATE-TIME") > -1 ) {
                    this.program = this.m3u8tag[o].split("\n");
                    this.programDateTimeOrig = this.program[0].replace(/EXT-X-PROGRAM-DATE-TIME:/, "");
                    if ( this.programDateTimeOrig.split(":").length - 1 < 3 ) {
                        this.insertValue = this.programDateTimeOrig.length - 2;
                        this.programDateTime = this.programDateTimeOrig.substr(0,this.insertValue) + ":" + this.programDateTimeOrig.substr(-2);
                    } else {
                        this.programDateTime = this.programDateTimeOrig;
                    }
                }
                //Offset Value
                if( this.m3u8tag[o].indexOf("EXTINF") > -1 ) {
                    this.lastFragLengthArr = this.m3u8tag[o].split(",");
                    this.lastFragLength = this.lastFragLengthArr[0].replace(/EXTINF:/, "");
                }
            }
            //Use Last EXT-X-PROGRAM-DATE-TIME + half last fragment length + delayTime;
            this.timeStamp = parseInt( new Date(this.programDateTime).getTime() + ( ( this.lastFragLength / 2 ) * 1000 ) - ( this.delayTime * 1000 ) );
            this.timecode = new Date(this.timeStamp);
        }

        HLSObj.prototype.m3u8URLParse = function( response ) {
            this.m3u8URLtag = response.split("#");
            this.firstLevel = null;
            for ( var o in this.m3u8URLtag) {
                //Get First Level EXT-X-STREAM-INF
                if( this.m3u8URLtag[o].indexOf("EXT-X-STREAM-INF") > -1 ) {
                    this.firstLevelarr = this.m3u8URLtag[o].split("\n");
                    for ( var v in this.firstLevelarr ) {
                        if ( this.firstLevelarr[v].indexOf("m3u8") > -1 ) {
                            this.firstLevel = this.firstLevelarr[v];
                            break;
                        }
                    }
                }
            }

            if ( this.firstLevel != null ) {
                if ( this.firstLevel.indexOf("http") > -1 ) {
                    this.m3u8Url = this.firstLevel;
                } else {
                    this.urlArr = this.url.split("/");
                    var re = new RegExp(this.urlArr[ this.urlArr.length - 1 ], "g");
                    this.m3u8Url = this.url.replace(re, this.firstLevel);
                }
            } else {
                this.m3u8Url = this.url;
            }
        }

        HLSObj.prototype.m3u8LastEXTINFParse = function( response ) {
            this.m3u8LastEXTINFtag = response.split("#");
            this.lastFragLength = null;
            for ( var o in this.m3u8LastEXTINFtag) {
                //Offset Value
                if( this.m3u8LastEXTINFtag[o].indexOf("EXTINF") > -1 ) {
                    this.lastFragLengthArr = this.m3u8LastEXTINFtag[o].split(",");
                    this.lastFragLengthTmp = this.lastFragLengthArr[0].replace(/EXTINF:/, "");
                    this.lastFragLength = this.lastFragLengthTmp.split("\n")[0];
                }
            }

            if ( this.lastFragLength != null ) {
                this.lastFragInterval = parseFloat(this.lastFragLength);
            } else {
                this.lastFragInterval = null;
            }
        }

        //NewPlayer(videoElementObject, HLS Address)
        // add newMexSeekSec by Lester on 2018-07-23
        HLSObj.prototype.InitConnect = function( videoid, url, newMexSeekSec ) {
            this.videoid = videoid;
            this.url = url;
            this.ResetChkVar();

            if ( this.Player === null ) {
                this.Player = document.getElementById(videoid);
                this.Player.defaultMuted = this.defaultMuted;
                this.Player.muted = this.defaultMuted;
                this.lastSoundState = this.defaultMuted;
                if ( this.iOS === false && this.hlsSupport === true ) {
                    this.hls = new Hls(this.varObj);
                    this.HLSEvent();
                    //this.ElementEventPlay();
                } else {
                    this.ElementEvent();
                    //this.ElementEventPlay();
                }
            } else {
                this.Player.defaultMuted = this.lastSoundState;
                this.Player.muted = this.lastSoundState;
				this.Player.poster = '';
            }
            
            // add newMexSeekSec by Lester on 2018-07-23
            if ( typeof newMexSeekSec !== undefined ) {
            	this.option.maxSeekSec = newMexSeekSec;
            	this.maxSeekSec = newMexSeekSec;

            	// hls.js seek delay 2 sec
            	if (this.hlsSupport === true && this.disableSeek === false ) {
                  	this.maxSeekSec = newMexSeekSec + 2;
            	}
            }
            // console.log("newMexSeekSec:" + newMexSeekSec + ", maxSeekSec:" + this.maxSeekSec);

            // console.log('url-->' + url );
            // this.StreamaSet( GetDomainStr(url), GetChnnaleStr(url) );

            if ( this.Player !== null && this.playState !== null ) {
                this.Restart(this.url);
            } else {
                this.Load(this.url);
            }
        }

        //HTMLMediaElement muted
        HLSObj.prototype.Mute = function() {
            if ( this.Player && this.useNativeVolume === false ) {
                if ( this.Player.muted === false ) {
                    this.Player.muted = true;
                    this.lastSoundState = true;
                    this.Player.defaultMuted = true;
                }
            }
        };

        //HTMLMediaElement unmuted
        HLSObj.prototype.unMute = function() {
            if ( this.Player && this.useNativeVolume === false ) {
                if( this.Player.muted === true ) {
                    this.Player.muted = false;
                    this.lastSoundState = false;
                    this.Player.defaultMuted = false;
                }
            }
        };

        //HTMLMediaElement Paused
        HLSObj.prototype.Pause = function( ctrl ) {
            if ( this.Player ) {
                if( this.Player.paused === false ) {
                    this.Player.pause();
                    this.manipause = true;
                }
            }
        };

        //HTMLMediaElement Resume
        HLSObj.prototype.Resume = function() {
            if ( this.Player ) {
                if( this.Player.paused === true ) {
                    this.Player.play();
                    this.manipause = false;
                }
            }
        };

        //HTMLMediaElement or hls.js load
        HLSObj.prototype.Load = function( url ) {
            if ( this.Player !== null && url !== null ) { 
                if ( this.iOS === true  || this.hlsSupport === false ) {
                    this.Player.src = url;
                    //Use Native Player directly call play
                    this.Player.play();
                    this.playState = true;
                } else {
                    this.hls.loadSource(url);
                    this.hls.attachMedia(this.Player);
                }
                this.doSeek = setInterval( this.ChkBuf.bind(this) , 2000 );
            }
        };

        //HTMLMediaElement Pause & Clear SRC
        HLSObj.prototype.Stop = function() {
            if ( this.Player ) {
                if ( this.playState === true ) {
                    clearInterval(this.doSeek);
                    if ( this.iOS === true  || this.hlsSupport === false ) {
                        this.Pause();
                    } else {
                        this.Pause();
                        this.hls.stopLoad();
                        this.hls.detachMedia();
                    }
                    this.url = null;
                    this.Player.src='';
                    this.playState = false;
                }
                this.ResetVar();
            }
        };

        //HTMLMediaElement  playbackrate
        HLSObj.prototype.PlaybackRate = function( speed ) {
            if ( this.Player ) {
                this.Player.playbackRate = speed;
            }
        };

        //HTMLMediaElement  volume (range 0->1)
        HLSObj.prototype.Volume = function( vol ) {
            if ( this.Player ) {
                this.Player.volume = vol;
            }
        };

        //HTMLMediaElement Seek iOS not Support
        HLSObj.prototype.Seek = function( seekSec ) {
            if ( this.Player ) {
                this.Player.currentTime = seekSec;
            }
        };

        //HTMLMediaElement  Restart
        HLSObj.prototype.Restart = function( url ) {
            this.ResetChkVar();
            if ( this.Player !== null ) {
                if ( this.iOS === false && this.hlsSupport === true ) {
                    clearInterval(this.doSeek);
                    this.ResetVar();
                    if( this.hls !== null ) {
                        //this.Pause();
                        this.hls.destroy();
                    }
                    this.playState = false;
                    this.hls = new Hls(this.varObj);
                    this.HLSEvent();
                    this.hls.loadSource(url);
                    this.hls.attachMedia(this.Player);
                } else {
                    clearInterval(this.doSeek);
                    this.ResetVar();
                    this.Pause();
                    this.Player.src = '';
                    this.playState = false;
                    this.Player.src = url;
                    //Use Native Player call play
                    this.Player.play();
                    this.playState = true;
                }
                    this.doSeek = setInterval( this.ChkBuf.bind(this) , 2000 );
            }
        };

        //HTMLMediaElement bufferedEnd, currentTime, buffered
        HLSObj.prototype.ChkBuf = function() {
            //Check init PTS Play;
            if ( this.iOS === false && this.hlsSupport === true ) {
                if ( this.maniInitPTS === 0 && this.fragParsingInit === 1 ) {
                    //console.log("Error: INIT PTS not found");
                    this.Restart(this.url);
                    return;
                }
            }

            //Check Buffered
            if( this.Player && this.url !== null && this.Player.readyState > 0 ) {
                this.bufferError = false;
                this.lastBuffer = this.bufferTime;
                this.lastCurrent = this.currentTime;
                //Uncaught DOMException: Failed to execute 'end' on 'TimeRanges': The index provided (4294967295) is greater than the maximum bound (0).
                this.bufferedLength = parseInt(this.Player.buffered.length) - 1;
                if ( this.bufferedLength < 0 ) {
                    // console.log('Error: Buffered length error');
                    this.bufferedLength = 0;
                }
                try {
                    this.bufferTime = parseFloat(this.Player.buffered.end( this.bufferedLength ));
                } catch (e) {
                    this.bufferTime = 0;
                    this.bufferError = true;
                } finally {
                    if( this.bufferTime === 0 ) {
                        this.bufferTime = this.lastBuffer;
                    }
                }

                this.currentTime = parseFloat(this.Player.currentTime);
                this.ready = this.Player.readyState;
                this.network = this.Player.networkState;

                if ( this.bufferError === false ) {
                    this.bufferTime = parseFloat(this.Player.buffered.end( this.bufferedLength ));
                    this.delayTime = this.bufferTime - this.currentTime;

                    // console.log('Buffered:' + this.bufferTime + ', Current:' + this.currentTime + ', Delay:' + this.delayTime);
                    if ( this.hlsSupport === true || this.iOS === true ) {
                        this.ChkSeek();
                    }
                }
            }

            //Check readyState, When readyState less than 10 --> restart
            if ( this.iOS === true || this.hlsSupport === false ) {
                if ( this.m3u8Url !== null ) {
                    this.ChkCacheLoop(this.m3u8Url);
                }

                if( this.readyTimes === this.maxReadyChk ) {
                    //console.log('Error: readyState Timeout Restart');
                    this.Restart(this.url);
                    return;
                }

                if( this.Player.readyState === 0 ) {
                    //this.Restart(this.url);
                    this.readyTimes += 1;
                } else if ( this.Player.readyState === 1) {
                    this.readyTimes += 1;
                } else {
                    this.readyTimes = 0;
                }
            }
        };

        //Chkseek
        HLSObj.prototype.ChkSeek = function() {
            if( this.Player && this.delayTime !== 0 ) {
                if ( this.disableSeek === false ) {
                    //Check delay then change Playbackrate
                    if ( this.delayTime > this.maxSeekSec + 10 ) {
                        //console.log("Delay > maxSeekSec + 10 Sec, restart connect now ");
                        this.Restart(this.url);
                        return;
                    }

                    // if ( this.delayTime < 0 ) {
                    //     console.log("DelayTime < 0 error, restart connect now");
                    //     this.Restart(this.url);
                    //     return;
                    // }

                    if ( this.delayTime < 0.5 ) {
                        //console.log("DelayTime < 0.5 pause wait buffer");
                        this.Player.pause();
                    } else if ( this.delayTime > 4 ) {
                        if ( this.manipause === false ) {
                            this.Player.play();
                        }
                    }
                
                    if( this.delayTime > this.maxSeekSec ) {
                        this.bufferTimes += 1;
                    } else {
                        this.bufferTimes = 0;
                    }

                    if ( this.bufferTimes > 2 ) {
                        if ( this.iOS === true) {
                            try {
                                this.seekableEnd = this.Player.seekable.end(0);
                            } catch (e) {
                                this.seekableEnd = 0;
                            }
                            if ( this.seekableEnd > this.Player.currentTime ) {
                                this.Seek(this.seekableEnd);
                            }
                        } else if ( this.hlsSupport === true ) {
                            this.seekTime = this.delayTime - this.maxSeekSec;
                            this.Seek(this.Player.currentTime + this.seekTime);
                        } else {
                            //This block is not Support hls.js (MSE) and not iOS Device.
                            //Those are usually Android Device use HTMLMediaElement API call Native Player.
                            //When Android use HTMLMediaElement API call Native Player that buffered.end and seekable.end is infinity, so that can't use currentTime.
                        }
                    }

                    //Call playback rate
                    // if ( this.Player.muted !== null ) {
                    //     if( this.bufferTimes > 2 ) {
                    //         //iOS not Support
                    //         //this.seekTime = this.delayTime - 8;
                    //         //this.Player.currentTime = this.Player.currentTime + this.seekTime;
                    //         if (this.hlsHidden === false) {
                    //             this.Resume();
                    //         }
                    //         if( this.useNativeVolume === false ) {
                    //             if( this.Player.muted === false ) {
                    //                 this.Player.muted = true;
                    //             }
                    //         }
                    //         this.PlaybackRate(2);
                    //         this.inSeek = true;
                    //     } else {
                    //         if ( this.inSeek === true ) {
                    //             this.PlaybackRate(1);
                    //             //console.log('seek lastTime muted:' + this.lastSoundState);
                    //             if( this.useNativeVolume === false ) {
                    //                 this.Player.muted = this.lastSoundState;
                    //             }
                    //         }
                    //     }
                    // }
                } else {
                    if ( this.Player.paused === true ) {
                        if ( this.delayTime > 4 ) {
                            if ( this.manipause === false ) {
                                this.Player.play();
                            }
                        }
                    }

                    if( this.delayTime > this.maxSeekSec ) {
                        this.bufferTimes += 1;
                    } else {
                        this.bufferTimes = 0;
                    }

                    if ( this.delayTime > this.maxSeekSec ) {
                        if( this.bufferTimes >= this.maxBufferOverChk ) {
                            //console.log("Delay > maxSeekSec Sec, restart connect now ");
                            this.Restart(this.url);
                            return;
                        }
                    }
                }

                //Check delay, if buffered not change then restart connect
                if ( this.lastTime >= this.maxBufferedChk ) {
                    //console.log("Buffered not change 10 Times, restart connect now");
                    this.Restart(this.url);
                    return;
                }

                if ( this.lastBuffer === this.bufferTime ) {
                    //console.log("Buffered not change");
                    this.lastTime += 1;
                } else {
                    this.lastTime = 0;
                }

                //Check init currentTime
                if ( this.manipause === false ) {
                    if ( this.currentStopTimes >= this.currentChk ) {
                        //console.log("currentTime stop currentChk times, restart now");
                        this.Restart(this.url);
                    }

                    if ( this.lastCurrent === this.currentTime ) {
                        this.currentStopTimes += 1;
                    } else {
                        this.currentStopTimes = 0;
                    }
                } else {
                    this.currentStopTimes = 0;
                }
            }
        };

        //HTMLMediaElement Events onplay
        HLSObj.prototype.ElementEventPlay = function() {
            if( this.Player ) {
                this.Player.onplay = function(){
                    if(isVisibilityChangeSupported()){
                        document.addEventListener(vendorVisibilityChange, function() {
                            if (document[vendorHidden]) {
                                //console.log("hidden");
                                _this.hlsHidden = true;
                                _this.Pause();
                            } else {
                                //console.log("show");
                                _this.hlsHidden = false;
                                _this.Resume();
                            }
                        });
                    }
                };
            }
        }

        // iOS Check SRS or Griffinmas Cahce Loop
        HLSObj.prototype.ChkCacheLoop = function( url ) {
            this.m3u8get = new this.HttpClient();
            var clientTime = new Date().getTime();
            if ( _this.lastclientTime == 0 || ( clientTime - _this.lastclientTime > 2000 )) {
                this.m3u8get.get( url, function(response) {
                    _this.basechk = btoa(response);
                });
                // When this time manifest content equal last time, check + 1
                if ( _this.basechk === _this.lastbasechk ) {
                    _this.basechktimes += 1;
                } else {
                    _this.basechktimes = 0;
                }
                _this.lastclientTime = clientTime;
            }

            this.lastbasechk = this.basechk;
        }

        //HTMLMediaElement Events on Error
        HLSObj.prototype.ElementEvent = function() {
            //HTMLMediaElement Error Code
            //MEDIA_ERR_ABORTED             1   The fetching of the associated resource was aborted by the user's request.
            //MEDIA_ERR_NETWORK             2   Some kind of network error occurred which prevented the media from being successfully fetched, despite having previously been available.
            //MEDIA_ERR_DECODE              3   Despite having previously been determined to be usable, an error occurred while trying to decode the media resource, resulting in an error.
            //MEDIA_ERR_SRC_NOT_SUPPORTED   4   The associated resource or media provider object (such as a MediaStream) has been found to be unsuitable.

            if( this.Player ) {
                this.Player.onerror = function() {
                    //console.log("Error " + this.Player.error.code + "; details: " + this.Player.error.message);
                    _this.errorcode = "Error " + _this.Player.error.code + "; details: " + _this.Player.error.message;
                    if ( _this.Player.error.code === 4 ) {
                        _this.Stop();
                        _this.errorcode  = 'Error: Manifest 404 not found';
                        _this.errDetails = 'Manifest Load Error, Stop Stream';
                    }
                }

                if ( this.iOS === true && this.hlsSupport === false ) {
                     this.Player.onloadstart = function() {
                        // if ( _this.useProgramDateTime === true || _this.useTimeCode === true ) {
                            _this.m3u8get = new _this.HttpClient();
                            _this.m3u8get.get( _this.url, function(response) {
                                _this.m3u8URLParse(response);
                            });
                        // }
                    }

                    this.Player.onprogress = function() {
                        if ( _this.useTimeCode === true ) {
                            //_this.timeStamp = parseInt( event.timeStamp - ( ( _this.delayTime + ( _this.lastFragInterval / 1.2 ) ) * 1000 ));
                            //_this.timecode = new Date(_this.timeStamp);
                            _this.m3u8get = new _this.HttpClient();
                            _this.m3u8get.get( _this.m3u8Url, function(response) {
                                _this.m3u8LastEXTINFParse(response);
                            });
                            if ( _this.lastFragInterval != null && _this.delayTime > 0 ) {
                                var delay = parseFloat( _this.Player.buffered.end( parseInt( _this.Player.buffered.length ) - 1 ) ) - parseFloat( _this.Player.currentTime );
                                var lastTimeStamp = _this.timeStamp;
                                if ( _this.m3u8modify != null ) {
                                    _this.timeStamp = _this.m3u8modify - parseInt( delay * 1000 );
                                    if ( lastTimeStamp > _this.timeStamp ) {
                                        _this.timeStamp = lastTimeStamp;
                                    }
                                    _this.timecode = new Date(_this.timeStamp);
                                } else {
                                    _this.timeStamp = parseInt( event.timeStamp - ( ( delay + ( _this.lastFragInterval / 1.2 ) ) * 1000 ));
                                    _this.timecode = new Date(_this.timeStamp);
                                }
                            }
                        }

                        if ( _this.useProgramDateTime === true ) {
                            _this.m3u8get = new _this.HttpClient();
                            _this.m3u8get.get( _this.m3u8Url, function(response) {
                                _this.m3u8Parse(response);
                            });
                        }
                    }
                }
            }
        }

        //ResetVar Reset Variable
        HLSObj.prototype.ResetVar = function() {
            this.bufferTime = 0, this.currentTime = 0, this.delayTime = 0, this.seekTime = 0, this.times = 0, this.lastTime = 0, this.lastBuffer = 0, this.seekableEnd = 0, this.lastCurrent = 0, this.readyTimes = 0, this.bufferTimes = 0, this.currentStopTimes = 0, this.errorcode = '';
            this.maniLoading = 0, this.maniLoaded = 0, this.maniInitPTS = 0, this.maniPaesed = 0, this.fragParsingInit = 0, this.manipause = false, this.inSeek = false, this.timecode = null, this.timeStamp = null, this.m3u8Url = null, this.lastFragInterval = null, this.m3u8modify = null;
        }

        //ResetVar Reset Check Error Variable
        HLSObj.prototype.ResetChkVar = function() {
            this.lastbasechk = '', this.basechk = '', this.basechktimes = 0, this.errDetails = '', this.updateTimes = 0;
        }

        //hls.js Events
        HLSObj.prototype.HLSEvent = function() {
            if( this.hls ) {
                // Identifier for an error event
                this.error = this.hls.on(Hls.Events.ERROR,function(event,data) {
                    _this.errorcode = data.details;

                    if (data.fatal) {
                        switch(data.type) {
                            case Hls.ErrorTypes.NETWORK_ERROR:
                                switch(data.details) {
                                    case Hls.ErrorDetails.MANIFEST_LOAD_ERROR:
                                        // raised when manifest loading fails because of a network error
                                        //console.log("Network Error: MANIFEST_LOAD_ERROR");
                                        _this.errDetails = 'Manifest Load Error, Stop Stream';
                                        _this.hls.recoverMediaError();
                                        break;
                                    case Hls.ErrorDetails.FRAG_LOAD_ERROR:
                                        // raised when fragment loading fails because of a network error
                                        //console.log("Network Error: FRAG_LOAD_ERROR");
                                        _this.errDetails = 'Manifest Load Timeout, Stream';
                                        _this.hls.recoverMediaError();
                                        break;
                                    default:
                                        // other error
                                        break;
                                }
                                if ( _this.maniInitPTS !== 0  && _this.fragParsingInit !== 1) {
                                    _this.ResetVar();
                                    _this.hls.startLoad();
                                }

                                break;
                            case Hls.ErrorTypes.MEDIA_ERROR:
                                _this.Restart(_this.url);
                                break;
                            case Hls.ErrorTypes.OTHER_ERROR:
                                break;
                            default:
                                _this.hls.destroy();
                                break;
                        }
                    } else {
                        switch(data.type) {
                            case Hls.ErrorTypes.NETWORK_ERROR:
                                break;
                            case Hls.ErrorTypes.MEDIA_ERROR:
                                switch(data.details) {
                                    case Hls.ErrorDetails.BUFFER_NUDGE_ON_STALL:
                                        // raised after hls.js seeks over a buffer hole to unstuck the playback
                                        //console.log("Media Error: BUFFER_NUDGE_ON_STALL");
                                        _this.ResetVar();
                                        _this.hls.startLoad();
                                        break;
                                    default:
                                        // other error
                                        break;
                                }
                                break;
                            case Hls.ErrorTypes.MUX_ERROR:
                                break;
                            case Hls.ErrorTypes.OTHER_ERROR:
                                break;
                            default:
                                // other recover
                                break;    
                        }
                    }
                });

                // fired to signal that a manifest loading starts
                this.hls.on(Hls.Events.MANIFEST_LOADING,function(event,data) {
                //     console.log("State: MANIFEST_LOADING");
                    _this.maniLoading = 1;
                    // data: { url : manifestURL }
                //     console.log(data);
                });

                // fired after manifest has been loaded
                this.hls.on(Hls.Events.MANIFEST_LOADED,function(event,data) {
                //     console.log("State: MANIFEST_LOADED");
                    _this.maniLoaded = 1;
                    // data: { levels : [available quality levels], audioTracks : [ available audio tracks], url : manifestURL, stats : { trequest, tfirst, tload, mtime}}
                //     console.log(data);
                });

                // fired after manifest has been parsed
                this.hls.on(Hls.Events.MANIFEST_PARSED,function() {
                //     console.log("State: MANIFEST_PARSED");
                    _this.maniPaesed = 1;
                    // data: { levels : [ available quality levels ], firstLevel : index of first quality level appearing in Manifest }
                //     console.log(data);
                });

                // fired when a level playlist loading finishes
                this.hls.on(Hls.Events.LEVEL_LOADED,function(event,data) {
                    //console.log("State: LEVEL_LOADED");
                    // data: { details : levelDetails object (please see below for more information), level : id of loaded level, stats : { trequest, tfirst, tload, mtime } }
                    // console.log(data);
                    var clientTime = new Date().getTime();
                    if ( _this.lastclientTime == 0 || ( clientTime - _this.lastclientTime > 2000 )) {
                        // Get manifest to base64
                        _this.basechk = btoa(data.networkDetails.responseText);
                        // When this time manifest content equal last time, check + 1
                        if ( _this.basechk === _this.lastbasechk ) {
                            _this.basechktimes += 1;
                        } else {
                            _this.basechktimes = 0;
                        }
                        _this.lastclientTime = clientTime;
                    }


                    // When the same content equal 5 times, player stop
                    if ( _this.basechktimes >= 5 ) {
                        _this.Stop();
                        _this.errorcode  = 'Error: Griffinamas or SRS Temp Fragment Loop';
                        _this.errDetails = 'Fragment Loop';
                        //console.log(_this.errorcode);
						_this.Player.poster = 'images/over.png';
                    }

                    _this.lastbasechk = _this.basechk;

                    if ( _this.useProgramDateTime === true ) {
                        if ( _this.ProgramDateTimeType === 'manifest' ) {
                            if ( typeof data.details.programDateTime != "undefined" ) {
                                // get last fragment duration
                                this.fragDuration = ( data.details.fragments[data.details.fragments.length - 1].duration ) * 1000;
                                _this.timeStamp = new Date(data.details.programDateTime).getTime() + this.fragDuration;
                                _this.timecode = new Date(_this.timeStamp);  
                            }
                        }
                    }
                });

                // fired when the first timestamp is found
                this.hls.on(Hls.Events.INIT_PTS_FOUND,function(event,data) {
                //     console.log("State: INIT_PTS_FOUND");
                    _this.maniInitPTS = 1;
                //     // data: { d : demuxer id, initPTS: initPTS , frag : fragment object }
                //     console.log(data);
                });

                // fired when Init Segment has been extracted from fragment
                this.hls.on(Hls.Events.FRAG_PARSING_INIT_SEGMENT,function(event,data) {
                //     console.log("State: FRAG_PARSING_INIT_SEGMENT");
                    _this.fragParsingInit = 1;
                    //hls.js call play on FRAG_PARSING_INIT_SEGMENT event
                    _this.Player.play();
                    _this.playState = true;
                //     // data: { id: demuxer id, frag : fragment object, moov : moov MP4 box, codecs : codecs found while parsing fragment }
                //     console.log(data);
                });

                // fired when fragment matching with current video position is changing
                this.hls.on(Hls.Events.FRAG_CHANGED,function(event,data) {
                    // console.log("State: FRAG_CHANGED");
                    if ( _this.useTimeCode === true ) {
                        this.cTime = new Date().getTime();
                        //_this.timeStamp = parseInt(this.cTime - ( _this.delayTime  * 1000 ));
                        _this.timeStamp = parseInt(this.cTime - ( _this.delayTime + ( data.frag.duration / 1.2 )  ) * 1000);
                        _this.timecode = new Date(_this.timeStamp);
                    }

                    if ( _this.useProgramDateTime === true ) {
                        if ( _this.ProgramDateTimeType === 'fragment' ) {
                            if ( typeof data.frag.programDateTime != "undefined" ) {
                                // _this.timeStamp = data.frag.rawProgramDateTime;
                                this.fragDuration = data.frag.duration * 1000;
                                // _this.timeStamp = new Date(data.frag.rawProgramDateTime).getTime() + this.fragDuration;
                                _this.timeStamp = new Date(data.frag.rawProgramDateTime).getTime();
                                _this.timecode = new Date(_this.timeStamp);
                            }
                        }
                    }
                    // console.log('Current DateTime:' + _this.timecode);
                    // data: { id : demuxer id, frag : fragment object }
                    // console.log(data);
                });

                // triggered when FPS drop in last monitoring period is higher than given threshold
                this.hls.on(Hls.Events.FPS_DROP,function(event,data) {
                //     console.log("State: FPS_DROP");
                //     // data: { curentDropped : nb of dropped frames in last monitoring period, currentDecoded : nb of decoded frames in last monitoring period, totalDroppedFrames : total dropped frames on this video element }
                //     console.log(data);
                });

                // triggered when FPS drop triggers auto level capping
                this.hls.on(Hls.Events.FPS_DROP_LEVEL_CAPPING,function(event,data) {
                //     console.log("State: FPS_DROP_LEVEL_CAPPING");
                //     // data: { level: suggested new auto level capping by fps controller, droppedLevel : level has too many dropped frames and will be restricted }
                //   console.log(data);
                });

                // fired when hls.js instance starts destroying. Different from MEDIA_DETACHED as one could want to detach and reattach a video to the instance of hls.js to handle mid-rolls for example
                this.hls.on(Hls.Events.DESTROYING,function(event,data) {
                    //console.log("State: DESTROYING");
                });
            }
        }
    }
        return HLSObj;
}();

