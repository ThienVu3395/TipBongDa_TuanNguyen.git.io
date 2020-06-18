<?php

include '../model/class.historyChangeData.php';
include '../model/class.account_info.php';
include '../model/nocsrf.php';
$historyChangeData = new HistoryChangeData();
$account_infoModel = new Account_Info();
$view_data['view_name'] = "historyChangeData/index.php";
$view_data['section_styles'] = "historyChangeData/style_index.php";
$view_data['section_scripts'] = "historyChangeData/script_index.php";
switch ($action) {
    case "menu":
    {
        $view_data['title'] = "Lịch Sử Thay Đổi Thông Tin Thành Viên";
        $view_data['view_name'] = "historyChangeData/menu.php";
        break;
    }
    case "nExtPoint2": {
        $view_data['title'] = "Lịch Sử Thay Đổi Giọt Máu";
        if (count($_POST) > 0) {
            if(!isset($_POST['columnChange']))
            {
                if (!empty($_POST['date'])) {
                    $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $historyChangeData->ViewHistoryChangeData('nExtPoint2',$cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeData.php';
                exit();
            }
            else 
            {
                $result = false;
                $user = array();
                if((int)$_POST['newValue'] >= 0)
                {
                    $user[$_POST['columnChange']] = $_POST['newValue'];
                    $result = $account_infoModel->Update($user, $_POST['cAccName']);
                    if($result)
                    {
                        $historyChangeData->Add($_POST['cAccName'], $_POST['columnChange'], $_POST['oldValue'], $_POST['newValue'], $_SESSION['userlogged'][6]);
                    }
                }
                header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
            }
        }
        $fromDate = date('d/m/Y') . " 00:00:00";
        $toDate = date('d/m/Y') . " 23:59:59";
        $view_data['model'] = $historyChangeData->ViewHistoryChangeData('nExtPoint2','', $fromDate, $toDate);
        break;
    }
    case "nExtPoint3": {
        $view_data['title'] = "Lịch Sử Thay Đổi Điểm OTP";
        if (count($_POST) > 0) {
            if(!isset($_POST['columnChange']))
            {
                if (!empty($_POST['date'])) {
                    $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $historyChangeData->ViewHistoryChangeData('nExtPoint3',$cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeData.php';
                exit();
            }
            else 
            {
                if((int)$_POST['newValue'] >= 0)
                {
                    $result = false;
                    $user = array();
                    $user[$_POST['columnChange']] = $_POST['newValue'];
                    $result = $account_infoModel->Update($user, $_POST['cAccName']);
                    if($result)
                        $historyChangeData->Add($_POST['cAccName'], $_POST['columnChange'], $_POST['oldValue'], $_POST['newValue'], $_SESSION['userlogged'][6]);
                }
                header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
            }
        }
        $fromDate = date('d/m/Y') . " 00:00:00";
        $toDate = date('d/m/Y') . " 23:59:59";
        $view_data['model'] = $historyChangeData->ViewHistoryChangeData('nExtPoint3','', $fromDate, $toDate);
        break;
    }
    case "Pass1": {
        if($_SESSION['userlogged'][6] != 'tuannguyen' && $_SESSION['userlogged'][6] != 'letuan' && $_SESSION['userlogged'][6] != 'quocdungml2019') 
        {
             header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
        }
        $view_data['title'] = "Lịch Sử Thay Đổi Pass 1";
        if (count($_POST) > 0) {
            if(!isset($_POST['columnChange']))
            {
                if (!empty($_POST['date'])) {
                    $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $historyChangeData->ViewHistoryChangeData('Pass1',$cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeData.php';
                exit();
            }
            else 
            {
                $user = $account_infoModel->GetByCAccName($_POST['cAccName']);
                $result = $account_infoModel->ChangePassword($_POST['cAccName'], $_POST['newValue']);
                if($result)
                    $historyChangeData->Add($_POST['cAccName'], $_POST['columnChange'], $user['PassCap1'], $_POST['newValue'], $_SESSION['userlogged'][6]);

                header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
            }
        }
        $fromDate = date('d/m/Y') . " 00:00:00";
        $toDate = date('d/m/Y') . " 23:59:59";
        $view_data['model'] = $historyChangeData->ViewHistoryChangeData('Pass1','', $fromDate, $toDate);
        break;
    }
    case "Pass2": {
        if($_SESSION['userlogged'][6] != 'tuannguyen' && $_SESSION['userlogged'][6] != 'letuan' && $_SESSION['userlogged'][6] != 'quocdungml2019') 
        {
             header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
        }
        $view_data['title'] = "Lịch Sử Thay Đổi Pass 2";
        if (count($_POST) > 0) {
            if(!isset($_POST['columnChange']))
            {
                if (!empty($_POST['date'])) {
                    $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $historyChangeData->ViewHistoryChangeData('Pass2',$cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeData.php';
                exit();
            }
            else 
            {
                $user = $account_infoModel->GetByCAccName($_POST['cAccName']);
                $result = $account_infoModel->UpdatePassword2($_POST['cAccName'], $_POST['newValue']);
                
                if($result)
                    $historyChangeData->Add($_POST['cAccName'], $_POST['columnChange'], $user['PassCap2'], $_POST['newValue'], $_SESSION['userlogged'][6]);
                
                header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
            }
        }
        $fromDate = date('d/m/Y') . " 00:00:00";
        $toDate = date('d/m/Y') . " 23:59:59";
        $view_data['model'] = $historyChangeData->ViewHistoryChangeData('Pass2','', $fromDate, $toDate);
        break;
    }
    case "cPhone": {
        $view_data['title'] = "Lịch Sử Thay Đổi Số Điện Thoại";
        if (count($_POST) > 0) {
            if(!isset($_POST['columnChange']))
            {
                if (!empty($_POST['date'])) {
                    $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $historyChangeData->ViewHistoryChangeData('cPhone',$cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeData.php';
                exit();
            }
            else 
            {
                $result = false;
                $user = array();
                $user[$_POST['columnChange']] = $_POST['newValue'];
                $result = $account_infoModel->Update($user, $_POST['cAccName']);
                if($result)
                    $historyChangeData->Add($_POST['cAccName'], $_POST['columnChange'], $_POST['oldValue'], $_POST['newValue'], $_SESSION['userlogged'][6]);
                header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
            }
        }
        $fromDate = date('d/m/Y') . " 00:00:00";
        $toDate = date('d/m/Y') . " 23:59:59";
        $view_data['model'] = $historyChangeData->ViewHistoryChangeData('cPhone','', $fromDate, $toDate);
        break;
    }  
    case "iLeftSecond": {
        $view_data['title'] = "Lịch Sử Thay Đổi Số Giây Chơi Còn Lại";
        if (count($_POST) > 0) {
            if(!isset($_POST['columnChange']))
            {
                if (!empty($_POST['date'])) {
                    $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $historyChangeData->ViewHistoryChangeData('iLeftSecond',$cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeData.php';
                exit();
            }
            else 
            {
                if((int)$_POST['newValue'] >= 0)
                {
                    $result = $account_infoModel->UpdateiLeftSecond($_POST['cAccName'], $_POST['newValue']);

                    if($result)
                        $historyChangeData->Add($_POST['cAccName'], $_POST['columnChange'], $_POST['oldValue'], $_POST['newValue'], $_SESSION['userlogged'][6]);
                }
                header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
            }
        }
        $fromDate = date('d/m/Y') . " 00:00:00";
        $toDate = date('d/m/Y') . " 23:59:59";
        $view_data['model'] = $historyChangeData->ViewHistoryChangeData('iLeftSecond','', $fromDate, $toDate);
        break;
    }     
    case "dEndDate": {
        $view_data['title'] = "Lịch Sử Thay Đổi Ngày Kết Thúc";
        if (count($_POST) > 0) {
            if(!isset($_POST['columnChange']))
            {
                if (!empty($_POST['date'])) {
                    $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $historyChangeData->ViewHistoryChangeData('dEndDate',$cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeData.php';
                exit();
            }
            else 
            {
                $user = $account_infoModel->GetByCAccName($_POST['cAccName']);
                $result = $account_infoModel->UpdatedEndDate($_POST['cAccName'], $_POST['newValue']);
                if($result)
                    $historyChangeData->Add($_POST['cAccName'], $_POST['columnChange'],  date_format($user['dEndDate'],'d/m/Y'), $_POST['newValue'], $_SESSION['userlogged'][6]);

                header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
            }
        }
        $fromDate = date('d/m/Y') . " 00:00:00";
        $toDate = date('d/m/Y') . " 23:59:59";
        $view_data['model'] = $historyChangeData->ViewHistoryChangeData('dEndDate','', $fromDate, $toDate);
        break;
    }  
    case "dBeginDate": {
        $view_data['title'] = "Lịch Sử Thay Đổi Ngày Bắt Đầu";
        if (count($_POST) > 0) {
            if(!isset($_POST['columnChange']))
            {
                if (!empty($_POST['date'])) {
                    $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
                    $date = $_POST['date'];
                    $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
                    $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
                } else {
                    $fromDate = date('d/m/Y') . " 00:00:00";
                    $toDate = date('d/m/Y') . " 23:59:59";
                }
                $view_data['model'] = $historyChangeData->ViewHistoryChangeData('dBeginDate',$cAccName, $fromDate, $toDate);
                include_once 'partial/_historyChangeData.php';
                exit();
            }
            else 
            {
                $user = $account_infoModel->GetByCAccName($_POST['cAccName']);
                $result = $account_infoModel->UpdatedBeginDate($_POST['cAccName'], $_POST['newValue']);
                if($result)
                    $historyChangeData->Add($_POST['cAccName'], $_POST['columnChange'],  date_format($user['dBeginDate'],'d/m/Y'), $_POST['newValue'], $_SESSION['userlogged'][6]);

                header("Location: ".base_url_admin."/user/details&cAccName=".$_POST['cAccName']);
            }
        }
        $fromDate = date('d/m/Y') . " 00:00:00";
        $toDate = date('d/m/Y') . " 23:59:59";
        $view_data['model'] = $historyChangeData->ViewHistoryChangeData('dBeginDate','', $fromDate, $toDate);
        break;
    }  
    
}

