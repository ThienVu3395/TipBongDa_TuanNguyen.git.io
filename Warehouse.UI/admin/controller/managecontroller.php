<?php 
  include '../model/class.account_info.php';
  include '../model/nocsrf.php';
  $account_infoModel = new Account_Info();
  switch($action)
  {
    case "otp":
    {
      $view_data['title'] = "Khóa/Bỏ Khóa OTP";
      $view_data['view_name'] = "manage/otp.php";  
      if(count($_POST) > 0)
      {
        try 
        {
          NoCSRF::check( 'csrf_token', $_POST, true, 60*10, false );
          if(empty($_POST['sdt']))
          {
            $view_data['errors'][] = "Bạn chưa nhập SĐT!";
          }
          if(empty($_POST['luachon']))
          {
            $view_data['errors'][] = "Bạn chưa lựa chọn phương thức!";
          }
          if(count($view_data['errors']) == 0)
          {
            $result = false;
            if($_POST['luachon'] == "khoa")
            {
              $result = $account_infoModel->LockOTP($_POST['sdt']);
              if($result)
              {
                header("Location: ".base_url_admin. "/manage/otp&message=Khóa thành công.");
              }
            }
            else if($_POST['luachon'] == "bokhoa")
            {
              $result = $account_infoModel->UnLockOTP($_POST['sdt']);
              if($result)
              {
                header("Location: ".base_url_admin. "/manage/otp&message=Bỏ Khóa thành công.");
              }
            }
            
          }
        }
        catch(Exception $ex)
        {
          $view_data['errors'][] = $ex->getMessage();
        }
      }
      break;
    }  
    case "lockuserbyphone":
    {
      $view_data['title'] = "Khóa Thành Viên Theo Điện Thoại";
      $view_data['view_name'] = "manage/lockuserbyphone.php";  
      if(count($_POST) > 0)
      {
        try 
        {
          NoCSRF::check( 'csrf_token', $_POST, true, 60*10, false );
          if(empty($_POST['sdt']))
          {
            $view_data['errors'][] = "Bạn chưa nhập SĐT!";
          }
          if(empty($_POST['luachon']))
          {
            $view_data['errors'][] = "Bạn chưa chọn cách thức!";
          }
          if(count($view_data['errors']) == 0)
          {
            $result = false;
            if($_POST['luachon'] == "khoa")
            {
              $result = $account_infoModel->LockUserByPhone($_POST['sdt']);
              header("Location: ".base_url_admin. "/manage/lockuserbyphone&message=Khóa thành công."); 
            }
            else if($_POST['luachon'] == "bokhoa")
            {
              $result = $account_infoModel->UnLockUserByPhone($_POST['sdt']);
              header("Location: ".base_url_admin. "/manage/lockuserbyphone&message=Bỏ Khóa thành công."); 
            }
            
          }
        }
        catch(Exception $ex)
        {
          $view_data['errors'][] = $ex->getMessage();
        }
      }
      break;
    }
  }

?>