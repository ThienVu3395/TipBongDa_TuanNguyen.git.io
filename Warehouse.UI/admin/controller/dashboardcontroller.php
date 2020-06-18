<?php 
  include '../model/class.account_info.php';
  include '../model/class.dashboard.php';
  $account_infoModel = new Account_Info();
  $dashboard_Model = new DashBoard();
  switch($action)
  {
  	case "index":
  	{
  		$view_data['title'] = "Bảng Điều Khiển";
  		$view_data['view_name'] = "dashboard/index.php";	
  		$view_data['topnapthe'] = $account_infoModel->GetTopCharge(10);
  		$view_data['recentCharge'] = $account_infoModel->GetHistoryCharge("",0,10);
  		$view_data['section_scripts'] = "dashboard/script_index.php";
  		$view_data['section_styles'] = "dashboard/style_index.php";
  		$view_data['totalAmountWithMonth'] = $dashboard_Model->GetTotalAmountWithMonth();
		  $view_data['CountCardChargeToday'] = $dashboard_Model->CountCardChargeToday();
		  $view_data['CountUsers'] = $account_infoModel->Count();
		  $view_data['GetTotalnExtPoint1'] = $dashboard_Model->GetTotalnExtPoint1();
		  $view_data['CountUsersOnline'] = $dashboard_Model->CountUsersOnline();
      $view_data['topnapxu'] = $dashboard_Model->GetTopNapXu(10);
  		break;
  	}
    case "GetCardChargeToday":
    {
      $view_data['title'] = "Danh Sách Nạp Xu Hôm Nay";
      $view_data['view_name'] = "dashboard/GetCardChargeToday.php";
      $view_data['model'] = $dashboard_Model->GetCardChargeToday();
      $view_data['section_scripts'] = "dashboard/script_index2.php";
      $view_data['section_styles'] = "dashboard/style_index2.php";
      break;
    }
    case "topnapthe":
    {
      $view_data['title'] = "Top Nạp Thẻ";
      $view_data['model'] = $account_infoModel->GetAllTopCharge();
      $view_data['view_name'] = "dashboard/TopNapThe.php";
      break;
    }
    case "topnapxu":
    {
      $view_data['title'] = "Top Nạp Xu";
      $view_data['model'] = $dashboard_Model->GetTopNapXu();
      $view_data['view_name'] = "dashboard/TopNapXu.php";
      break;
    }
  }

?>