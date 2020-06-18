<?php 
  include_once('../model/nocsrf.php');
  include_once('../model/class.account.php');
  $account = new Account();
  switch($action)
  {
    case "enterotp":
    {
      $view_data['title'] = "Xác thực OTP";
      $view_data["view_name"] = "account/enterotp.php";
      if(count($_POST) > 0)
      {
        $otp = $_POST['otp'];
        if(empty($otp))
        {
          $view_data['errors'][] = "Bạn chưa nhập mã OTP!";
        }
        else 
        {
          if(!isset($_SESSION['OTPForAdmin']) || $otp != $_SESSION['OTPForAdmin'])
            $view_data['errors'][] = "Mã OTP không đúng!";
        }
        if(count($view_data['errors']) == 0)
        {
          $account->CurrentOTP(md5($_POST['username']), $_SESSION['OTPForAdmin']);
          $user = $account->GetAdminFromUserName(md5($_POST['username']));
          $_SESSION['userlogged'] = $user;
          setcookie('OTPForAdmin', $otp, time() + (86400 * 1), "/"); 
          header('Location:'.base_url_admin);
          exit();
        }
      }
      break;
    }
  	case "adminlogoff":
  	{
        $view_data['title'] = "Đăng nhập";
        $view_data["view_name"] = "account/login.php";
        try
        {
            unset($_SESSION['userlogged']);
            header('Location: '.base_url);
            exit();
        }
        catch ( Exception $e )
        {
            $view_data['errors'][] = $e->getMessage();
        }   
        break;
  	}
  }

?>