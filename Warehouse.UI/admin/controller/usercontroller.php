<?php

include '../model/class.account_info.php';
include '../model/class.account.php';
include '../model/class.question.php';
include '../model/nocsrf.php';
$account_infoModel = new Account_Info();
$questionModel = new Question();
$accountModel = new Account();
$database_Model = new Database();
switch ($action) {
    case "index": {
            $view_data['title'] = "Danh Sách Thành Viên";
            $view_data['view_name'] = "user/index.php";
            $view_data['section_styles'] = "user/style_index.php";
            $view_data['section_scripts'] = "user/script_index.php";
            $view_data['totalPage'] = ceil($account_infoModel->Count() / 20.0);
            break;
        }
    case "userHasExtpoint1":
    {
            $view_data['title'] = "Danh Sách Thành Viên Có Xu";
            $view_data['view_name'] = "user/userHasExtpoint1.php";
            $view_data['model'] = $account_infoModel->GetListUserHasnExtpoint1();
            $view_data['section_styles'] = "user/style_index.php";
            $view_data['section_scripts'] = "user/script_userHasExtpoint1.php";
            break;
    }
    case "useronline": {
            $view_data['title'] = "Danh Sách Thành Viên Đang Online";
            $view_data['view_name'] = "user/useronline.php";
            $view_data['section_styles'] = "user/style_index.php";
            $view_data['section_scripts'] = "user/script_useronline.php";
            $view_data['model'] = $account_infoModel->GetListOnline();
            break;
        }
    case "paging": {
            $page = isset($_GET['page']) ? (int) $_GET['page'] : 1;
            $view_data['pageCurrent'] = $page;
            $view_data['model'] = $account_infoModel->GetList(20 * ($page - 1), 20);
            $view_data['totalPage'] = ceil($account_infoModel->Count() / 20.0);
            include 'partial/_listUser.php';
            exit();
        }
    case "changepassword": {
            if (count($_POST) > 0) {
                try {
                    if (empty($_POST['password'])) {
                        $view_data['errors'][] = "Bạn chưa nhập mật khẩu hiện tại!";
                    } else if (md5($_POST['password']) != $_SESSION['userlogged'][1]) {
                        $view_data['errors'][] = "Mật khẩu hiện tại không đúng!";
                    } else if (empty($_POST['newpassword'])) {
                        $view_data['errors'][] = "Bạn chưa nhập mật khẩu mới!";
                    } else if (strlen($_POST['newpassword']) < 8) {
                        $view_data['errors'][] = "Mật khẩu mới phải tối thiểu 8 ký tự!";
                    } else if ($_POST['newpassword'] != $_POST['confirmnewpassword']) {
                        $view_data['errors'][] = "Nhập lại Mật khẩu mới không khớp!";
                    }
                    if (count($view_data['errors']) == 0) {
                        $result = $accountModel->UpdatePassword1($_SESSION['userlogged'][0], md5($_POST['newpassword']));
                        if ($result) {
                            $_SESSION['userlogged'][1] = md5($_POST['newpassword']);
                            echo "<p class='text-success'>Đổi mật khẩu thành công.</p>";
                            exit();
                        }
                    }
                } catch (Exception $ex) {
                    $view_data['errors'][] = $ex->getMessage();
                }
            }
            echo ConvertListErrorToString($view_data['errors']);
            exit();
        }
    case "search": {
            $keyword = $_POST['keyword'];
            $view_data['keyword'] = $keyword;
            $view_data['pageCurrent'] = 1;
            if ($keyword != "") {
                $view_data['model'] = $account_infoModel->SearchByNameOrPhone($keyword);
                $view_data['totalPage'] = 1;
            } else {
                $view_data['pageCurrent'] = 1;
                $view_data['model'] = $account_infoModel->GetList(0, 20);
                $view_data['totalPage'] = ceil($account_infoModel->Count() / 20.0);
            }
            include 'partial/_listUser.php';
            exit();
        }
    case "searchOnPageDetail": {
            $view_data['model'] = $account_infoModel->GetByCAccName($_POST['keyword']);
            $view_data['columns'] = $database_Model->GetColumns('Account_Info');
            $view_data['questions'] = $questionModel->GetList();
            include 'partial/_detailUser.php';
            exit();
        }
    case "getIframeDetail": {
            $cAccName = $_GET['cAccName'];
            include 'partial/_iframeDetailUser.php';
            exit();
        }
    case "details": {
            $view_data['title'] = "Thông Tin Thành Viên";
            $view_data['view_name'] = "user/details.php";
            $view_data['section_scripts'] = "user/detail_script.php";
            $cAccName = $_GET['cAccName'];
            $view_data['model'] = $account_infoModel->GetByCAccName($cAccName);
            $view_data['columns'] = $database_Model->GetColumns('Account_Info');
            if (isset($_POST['cAccName'])) {
                try {
                    unset($_POST["cAccName"]);
                    $_POST['bIsBanned'] = isset($_POST['bIsBanned']) ? 1 : 0;
                    $param1 = $_POST;
                    NoCSRF::check('csrf_token', $_POST, true, 60 * 10, false);
                    if (count($view_data['errors']) == 0) {
                        $result1 = $account_infoModel->Update($param1, $cAccName);
                        if ($result1) {
                            header("Location: " . base_url_admin . "/user/details&cAccName=" . $cAccName . "&messageSuccess=Update successful");
                        }
                    }
                } catch (Exception $ex) {
                    $view_data['errors'][] = $ex->getMessage();
                }
            }
            break;
        }
    case "delete": {
            break;
        }
    case "changeBalance": {
            try {
                $view_data['title'] = "Thông Tin Thành Viên";
                $view_data['view_name'] = "user/details.php";
                $cAccName = $_POST['cAccName'];
                $changeType = $_POST['changeType'];
                $valueChange = (int) $_POST['valueChange'];
                $view_data['columns'] = $database_Model->GetColumns('Account_Info');
                $view_data['model'] = $account_infoModel->GetByCAccName($cAccName);

                if ($valueChange <= 0) {
                    $view_data['errors'][] = "Giá trị thay đổi phải > 0";
                } else if ($changeType == "Giảm xuống" && $view_data['model']['nExtPoint1'] < $valueChange) {
                    $view_data['errors'][] = "Giá trị thay đổi không thể lớn hơn số xu hiện tại";
                }
                if (count($view_data['errors']) == 0) {
                    $result = $account_infoModel->UpdateExtpoint1($changeType, $valueChange, $cAccName, $_SESSION['userlogged'][0]);
                    if ($result)
                        header("Location: " . base_url_admin . "/user/details&cAccName=" . $cAccName . "&messageSuccess=Update successful");
                }
            } catch (Exception $ex) {
                $view_data['errors'][] = $ex->getMessage();
            }
            break;
        }
    case "historychangebalance": {
            $view_data['section_styles'] = "user/style_history.php";
            $view_data['section_scripts'] = "user/script_history.php";
            $view_data['title'] = "Lịch Sử Nạp Xu";
            $view_data['view_name'] = "user/historychangebalance.php";
            $view_data['model'] = array();
            if (count($_POST) > 0) {
                $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                if (!empty($_POST['date'])) {
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $account_infoModel->ViewHistoryChangeBalance($cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeBalance.php';
                exit();
            }
            break;
        }
    case "historyCharge": {
            $view_data['section_styles'] = "user/style_index.php";
            $view_data['section_scripts'] = "user/script_historyCharge.php";
            $view_data['view_name'] = "user/historyCharge.php";
            $cAccName = isset($_GET['cAccName']) ? $_GET['cAccName'] : "";
            $view_data['title'] = !empty($_GET['cAccName']) ? "Lịch Sử Nạp Thẻ Của Tài Khoản " . $cAccName : "Lịch Sử Nạp Thẻ";
            $view_data['cAccName'] = $cAccName;
            $view_data['totalPage'] = ceil($account_infoModel->CountHistoryZing($cAccName) / 20.0);
            
            break;
        }
    case "pagingHistoryCharge": {
            $page = isset($_GET['page']) ? (int) $_GET['page'] : 1;
            $cAccName = !empty($_GET['cAccName']) ? $_GET['cAccName'] : "";
            $view_data['cAccName'] = $cAccName;
            $view_data['pageCurrent'] = $page;
            $view_data['model'] = $account_infoModel->GetHistoryCharge($cAccName, 20 * ($page - 1), 20);
            $view_data['totalPage'] = ceil($account_infoModel->CountHistoryZing($cAccName) / 20.0);
            // $view_data['totalAmount'] = (int)$account_infoModel->GetTotalHistoryCharge($cAccName);
            include 'partial/_historyCharge.php';
            exit();
        }
    case "topCharge": {
            $view_data['view_name'] = "user/topCharge.php";
            $view_data['title'] = "Top Nạp Thẻ";
            $view_data['model'] = $account_infoModel->GetTopCharge();
            break;
        }
}
