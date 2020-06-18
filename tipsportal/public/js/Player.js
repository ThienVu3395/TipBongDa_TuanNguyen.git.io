var StreamPlayer = function(uidx, mKey){
    this.Version = 'v 020.01.16.01';
    this.streama = new StreamA();
	this.uid = uidx;
	this.mKey = mKey;
	this.PlayerObj = new HLSJSAPI.Class({'UID':uidx, 'disableSeek':true, 'maxBufferedChk':30, 'maxReadyChk':10, 'useNativeVolume':true, 'maxSeekSec':100, 'defaultMuted':false, 'useNativePlayer':false});
	console.log("Player Version: " + this.Version);
};

StreamPlayer.prototype.InitConnect = function(elt, stname)
{
	this.stname = "m" + stname;
	this.vUrl = "https://tipsportal.pq93.com:8091/live/" + this.stname + "?uid=" + this.uid + "&chnnelName=" + this.stname + "&sMD5=" + this.mKey;
	console.log(this.vUrl);
	this.url = this.LineF + this.stname + this.LineL;
	this.PlayerObj.InitConnect(elt, this.vUrl);
	
	var streamInfo = {project:'Yongbao', streamname:this.stname, cdn:"https://tipsportal.pq93.com:8091", uid:this.uid, player:"HLS"};
    this.streama.send(streamInfo);
};
