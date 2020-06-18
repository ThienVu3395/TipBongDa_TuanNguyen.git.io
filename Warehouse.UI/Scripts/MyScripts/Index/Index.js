angular.module("CommonApp")
    .controller("Index", function ($scope, CommonController, $timeout, blockUI) {
        // Get Init In PagePartial For Home
        $scope.InitHome = function () {
            $scope.getRankTableForHome();
            $scope.countryId = 0;
            $scope.getCountry();
            var hamcho = function () {
                if ($scope.countryId == 0) {
                    $timeout(hamcho, 500);
                }
                else {
                    //document.getElementById("option" + $scope.countryId.country_id).className = "active";
                    $scope.leagueId = 0;
                    $scope.getLeague();
                    var hamcho2 = function () {
                        if ($scope.leagueId == 0) {
                            $timeout(hamcho2, 500);
                        }
                        else {
                            $scope.getTopScore();
                            $scope.MatchTable = [];
                            $scope.getNearlyMatch(41);
                            $scope.getNearlyMatch(46);
                            //$scope.getTopScore();
                            //let string = document.getElementById("title").innerHTML;
                            //if (string == "point table") {
                            //    $scope.getRankTable();
                            //}
                            //else if (string == "RESULT") {
                            //    $scope.getResult();
                            //}
                            //else if (string == "FIXTURE") {
                            //    $scope.getCalendar();
                            //}
                        }
                    }
                    $timeout(hamcho2, 500);
                }
            }
            $timeout(hamcho, 500);
        }

        // Get Nearly Match
        $scope.getNearlyMatch = function (countryID) {
            let currentdate = new Date();
            let fd = (currentdate.getDate());
            let fm = (currentdate.getMonth() + 1);
            let fy = currentdate.getFullYear();
            let today = fy + "-" + fm + "-" + fd;
            let url = CommonController.urlAPI.API_Result2;
            var res = CommonController.getDataTips(url + today + '&country_id=' + countryID);
            res.then(
                function success(response) {
                    for (let i = 0; i < response.data.length; i++) {
                        if (response.data[i].match_status == "Finished") {
                            $scope.MatchTable.push(response.data[i]);
                        }
                    }
                    console.log($scope.MatchTable);
                },
                function errorCallback(response) {
                    console.log(response.data.message)
                }
            );
        }

        // Get Ranked Tabled For Home
        $scope.getRankTableForHome = function () {
            var res = CommonController.getDataTips(CommonController.urlAPI.API_RankTable + "148");
            res.then(
                function success(response) {
                    $scope.RankTable = response.data;
                    $scope.RankTable.splice(20, $scope.RankTable.length);
                    $scope.LeagueName = $scope.RankTable[0].league_name;
                },
                function errorCallback(response) {
                    console.log(response.data.message)
                }
            );
        }

        // Get Top Scores For Home
        $scope.getTopScore = function () {
            var res = CommonController.getDataTopScore(CommonController.urlAPI.API_TopScores + "148");
            res.then(
                function success(response) {
                    $scope.TopScores = response.data.result;
                    $scope.TopScores.splice(10, $scope.TopScores.length);
                    for (let i = 0; i < $scope.TopScores.length; i++) {
                        var res2 = CommonController.getDataTopScore(CommonController.urlAPI.API_TopScoresImage + $scope.TopScores[i].team_key);
                        res2.then(
                            function success(response) {
                                $scope.TopScores[i].team_logo = response.data.result[0].team_logo;
                            },
                            function errorCallback(response) {
                                console.log(response.data.message)
                            }
                        );
                    }
                },
                function errorCallback(response) {
                    console.log(response.data.message)
                }
            );
        }

        // Get League + Data When Change Country For Home
        $scope.getLeagueHome = function (countryID) {
            $scope.countryId = {};
            $scope.countryId.country_id = countryID;
            $scope.leagueId = 0;
            $scope.getLeague();
            var hamcho = function () {
                if ($scope.leagueId == 0) {
                    $timeout(hamcho, 500);
                }
                else {
                    document.getElementById("option" + $scope.countryId.country_id).className = "active";
                    let string = document.getElementById("title").innerHTML;
                    if (string == "point table") {
                        $scope.getRankTable();
                    }
                    else if (string == "RESULT") {
                        $scope.getResult();
                    }
                    else if (string == "FIXTURE") {
                        $scope.getCalendar();
                    }
                }
            }
            $timeout(hamcho, 500);
        }

        // Get Countries
        $scope.getCountry = function () {
            var url = CommonController.urlAPI.API_Country;
            var res = CommonController.getDataTips(url);
            res.then(
                function success(response) {
                    $scope.listCountries = response.data;
                    $scope.countryId = $scope.listCountries[0];
                },
                function errorCallback(response) {
                    $scope.countryId = -1;
                    console.log(response.data.message)
                }
            );
        }

        // Get League
        $scope.getLeague = function () {
            var url = CommonController.urlAPI.API_League + $scope.countryId.country_id;
            var res = CommonController.getDataTips(url);
            res.then(
                function success(response) {
                    $scope.listLeague = response.data;
                    $scope.nameStanding = $scope.listLeague[0].league_name;
                    $scope.imgStanding = $scope.listLeague[0].league_logo;
                    $scope.leagueId = $scope.listLeague[0];
                },
                function errorCallback(response) {
                    $scope.leagueId = -1;
                    console.log(response.data.message)
                }
            );
        }

        // Get Ranked Table
        $scope.getRankTable = function () {
            var res = CommonController.getDataTips(CommonController.urlAPI.API_RankTable + $scope.leagueId.league_id);
            res.then(
                function success(response) {
                    $scope.RankTable = response.data;
                    $scope.teamId = $scope.RankTable[0];
                },
                function errorCallback(response) {
                    console.log(response.data.message)
                }
            );
        }

        // Get Result
        $scope.getResult = function () {
            let fd = $scope.FromDate.getDate();
            let fm = $scope.FromDate.getMonth() + 1;
            let fy = $scope.FromDate.getUTCFullYear();
            let td = $scope.ToDate.getDate();
            let tm = $scope.ToDate.getMonth() + 1;
            let ty = $scope.ToDate.getUTCFullYear();
            let formDate = fy + "-" + fm + "-" + fd;
            let toDate = ty + "-" + tm + "-" + td;
            let url = CommonController.urlAPI.API_Result + formDate + '&to=' + toDate + '&league_id=' + $scope.leagueId.league_id;
            var res = CommonController.getDataTips(url);
            res.then(
                function success(response) {
                    $scope.listResult = response.data;
                    console.log($scope.listResult)
                },
                function errorCallback(response) {
                    console.log(response.data.message)
                }
            );
        }

        // Get Calendar
        $scope.getCalendar = function () {
            let url = CommonController.urlAPI.API_Fixture + $scope.leagueId.league_id;
            var res = CommonController.getDataTips(url);
            res.then(
                function success(response) {
                    if (Array.isArray(response.data)) {
                        $scope.listResult = response.data;
                        console.log($scope.listResult);
                        $scope.tt = true;
                    }
                    else {
                        $scope.listResult = response.data.message;
                        $scope.tt = false;
                        alert($scope.listResult);
                    }
                },
                function errorCallback(response) {
                    console.log(response.data.message)
                }
            );
        }

        // For ADmin /////////////////////////////////////////////////////////////////////////////

        // Init For Admin
        $scope.InitForAdmin = function (status) {
            $scope.countryId = 0;
            $scope.getCountry();
            var hamcho = function () {
                if ($scope.countryId == 0) {
                    $timeout(hamcho, 500);
                }
                else {
                    $scope.leagueId = 0;
                    $scope.getLeague();
                    var hamcho2 = function () {
                        if ($scope.leagueId == 0) {
                            $timeout(hamcho2, 500);
                        }
                        else {
                            if (status == "goldengold") {
                                $scope.getTeam();
                            }
                            else if (status == "ranktable") {
                                $scope.getRankTable();
                            }
                            else if (status == "result") {
                                $scope.getEvent();
                            }
                            else if (status == "calendar") {
                                $scope.getEvent();
                            }
                        }
                    }
                    $timeout(hamcho2, 500);
                }
            }
            $timeout(hamcho, 500);
        }

        // GetLeague Admin
        $scope.getLeagueAdmin = function (status) {
            $scope.leagueId = 0;
            $scope.getLeague();
            var hamcho = function () {
                if ($scope.leagueId == 0) {
                    $timeout(hamcho, 500);
                }
                else {
                    if (status == "goldengold") {
                        $scope.getTeam();
                    }
                    else if (status == "ranktable") {
                        $scope.getRankTable();
                    }
                    else if ('result') {
                        $scope.getEvent('old');
                    }
                    else if ('result') {
                        $scope.getEvent('calendar');
                    }
                }
            }
            $timeout(hamcho, 500);
        }

        // Get TeamInfo
        $scope.getTeam = function () {
            var res = CommonController.getDataTips(CommonController.urlAPI.API_TeamInfo + $scope.leagueId.league_id);
            res.then(
                function success(response) {
                    $scope.ListTeam = response.data;
                    $scope.ListPlayer = $scope.ListTeam[0].players;
                    $scope.teamId = $scope.ListTeam[0];
                },
                function errorCallback(response) {
                    console.log(response.data.message)
                }
            );
        }

        // Get Players
        $scope.getPlayers = function () {
            $scope.ListPlayer = $scope.teamId.players;
        }

        // Get Date Format
        function GetDateTimeFormat(string) {
            let d = string[0] + string[1];
            let m = string[3] + string[4];
            let y = string[6] + string[7] + string[8] + string[9];
            return y + "-" + m + "-" + d;
        }

        // Get Events 
        $scope.getEvent = function () {
            let s = document.getElementById("daterangepicker_start").value;
            $scope.startDate = GetDateTimeFormat(s);
            let e = document.getElementById("daterangepicker_end").value;
            $scope.endDate = GetDateTimeFormat(e);
            document.getElementById("reservation").value = s + "-" + e;
            let param = $scope.startDate + "&to=" + $scope.endDate + "&league_id=" + $scope.leagueId.league_id + "&APIkey=" + CommonController.urlAPI.keyAPI;
            var url = CommonController.urlAPI.API_Result + param;
            var res = CommonController.getDataTips(url);
            blockUI.start();
            res.then(
                function success(response) {
                    if (Array.isArray(response.data)) {
                        $scope.listEvent = response.data;
                        $scope.tt = true;
                    }
                    else {
                        $scope.tt = false;
                        swal({
                            title: 'Xin Lỗi',
                            text: 'Không Tìm Thấy Thông Tin',
                            icon: 'error',
                        });
                    }
                    blockUI.stop();
                },
                function errorCallback(response) {
                    $scope.countryId = -1;
                    console.log(response.data.message)
                }
            );
        }

        // Get Data For Standing + Fixture Board
        $scope.parseInt = parseInt;
        $scope.tt = false;
    })