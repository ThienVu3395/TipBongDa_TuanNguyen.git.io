//angular.module("CommonApp", ['ngSanitize', 'blockUI', 'ngMessages'])
angular.module("CommonApp", ['blockUI'])
    //Cấu hình cho blockUI
    .config(function (blockUIConfig) {
        'blockUI',
            blockUIConfig.delay = 200;
        blockUIConfig.autoBlock = true;
        blockUIConfig.blockBrowserNavigation = false;
    })
    .factory("CommonController", ["$http",
        function ($http) {
            // Tạo UrlAPI động
            var baseURL = window.location.protocol + "//" + window.location.host + "/";
            var appURL = { pathAPI: baseURL };

            //Hàm Lấy Ngày Tháng Năm
            this.getDateMonthYear = () => {
                var currentdate = new Date();
                var dateMonthYear = currentdate.getFullYear() + "-" + (currentdate.getMonth() + 1) + "-" + (currentdate.getDate());
                return dateMonthYear;
            };

            // API TIPS FOOTBALL
            var UrlAPI = {
                keyAPI: 'f9617bfb9d3f9845f85603549fbc70b5e8564ea3bed743f46104fa24f09305cb',            
                API_RankTable: 'https://apiv2.apifootball.com/?action=get_standings&league_id=',
                API_TeamInfo: 'https://apiv2.apifootball.com/?action=get_teams&league_id=',
                API_PlayerInfo: 'https://apiv2.apifootball.com/?action=get_players&player_name=',
                API_Country: 'https://apiv2.apifootball.com/?action=get_countries',
                API_League: 'https://apiv2.apifootball.com/?action=get_leagues&country_id=',
                API_Fixture: 'https://apiv2.apifootball.com/?action=get_events&timezone=Asia/Ho_Chi_Minh&from=' + this.getDateMonthYear() + '&to=' + this.getDateMonthYear() + '&league_id=',
                API_Result: 'https://apiv2.apifootball.com/?action=get_events&timezone=Asia/Ho_Chi_Minh&from=',
                API_Result2: 'https://apiv2.apifootball.com/?action=get_events&timezone=Asia/Ho_Chi_Minh&from=2020-03-01&to=',

                keyAPI_TopScores: '006e102dbe165751d5ac98764be631f436b07fac6c7bbe0a5edb066cb1e894e4',
                API_TopScores: 'https://allsportsapi.com/api/football/?met=Topscorers&&leagueId=',
                API_TopScoresImage: 'https://allsportsapi.com/api/football/?&met=Teams&teamId=',
            }

            // Hàm Get API TIPS FOOTBALL
            this.getDataTips = (Url) => {
                var res = $http({
                    url: Url + "&APIkey=" + UrlAPI.keyAPI,
                    method: 'GET',
                });
                return res;
            };

            // Hàm Get API TopScore
            this.getDataTopScore = (Url) => {
                var res = $http({
                    url: Url + "&APIkey=" + UrlAPI.keyAPI_TopScores,
                    method: 'GET',
                });
                return res;
            };

            // Hàm Post API
            this.postData = (urlAPI, Data) => {
                var res = $http({
                    url: appURL.pathAPI + urlAPI,
                    method: 'POST',
                    data: Data,
                    headers: { 'Content-Type': 'application/json' }
                })
                return res
            };

            return {
                urlAPI: UrlAPI,
                getDataTips: this.getDataTips,
                getDataTopScore: this.getDataTopScore,
                postData: this.postData,
                //postDataEC: this.postDataEC,
                //captcha: this.getCaptcha,
            }
        }]);