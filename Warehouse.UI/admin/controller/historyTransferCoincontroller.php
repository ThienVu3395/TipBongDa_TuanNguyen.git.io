<?php 
  include '../model/class.HistoryTransferCoin.php';
  $HistoryTransferCoin_Model = new HistoryTransferCoin();
  switch($action)
  {
  	case "index":
  	{
  		$view_data['title'] = "Lịch Sử Chuyển Xu";
  		$view_data['view_name'] = "historytransfercoin/index.php";	
  		$view_data['section_scripts'] = "historytransfercoin/script_index.php";
  		$view_data['section_styles'] = "historytransfercoin/style_index.php";

      if (count($_POST) > 0) {
          // $cAccName = isset($_POST['cAccName']) ? $_POST['cAccName'] : "";
          if (!empty($_POST['date'])) {
              $date = $_POST['date'];
              $fromDate = trim(explode('-', $date)[0]) . " 00:00:00";
              $toDate = trim(explode('-', $date)[1]) . " 23:59:59";
          } else {
              $fromDate = date('d/m/Y') . " 00:00:00";
              $toDate = date('d/m/Y') . " 23:59:59";
          }
          $view_data['model'] = $HistoryTransferCoin_Model->GetList($fromDate, $toDate);
          include_once 'partial/_historytransfercoin.php';
          exit();
      }
      $fromDate = date('d/m/Y') . " 00:00:00";
      $toDate = date('d/m/Y') . " 23:59:59";
      $view_data['model'] = $HistoryTransferCoin_Model->GetList($fromDate, $toDate);
  		break;
  	}
    
  }

?>