<?php 
  include '../model/class.popup.php';
  include '../model/nocsrf.php';
  $popupModel = new Popup();
  switch($action)
  {
  	case "index":
  	{
  		$view_data['title'] = "Bảng Tin";
  		$view_data['view_name'] = "popup/index.php";
  		$view_data['section_scripts'] = "popup/script_index.php";
  		$view_data['section_styles'] = "popup/style_index.php";
  		$view_data['model'] = $popupModel->Get();
      if(count($_POST) > 0)
      {
        $result = $popupModel->Edit($_POST['Content'], isset($_POST['Display']), (int)$_POST['Timeout'] * 1000);
        header("Location: ".base_url_admin."/popup");
      }
  		break;
  	}
    
  }

?>