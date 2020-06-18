<?php 
	session_start(); 
	include_once '../model/config.php';
	include_once '../model/connection.php';
    require_once '../model/class.database.php';
	include_once('../model/nocsrf.php');
	include_once('../model/class.account.php');
	$account = new Account();
	$view_data = array();
    $view_data['errors'] = array();
    if(isset($_POST['userlogin']) && isset($_POST['passlogin']))
    {
        if($_POST["userlogin"] != "" &&  $_POST["passlogin"] != "")
        {
            try
            {
                $user = $account->AdminLogin(md5($_POST["userlogin"]), md5($_POST["passlogin"]));
                if($user != null) 
                {  
                    if(!isset($_COOKIE['OTPForAdmin']))
                    {      

                        $OTP = rand(123456, 999999);
                        $content = "Ma OTP cua ban la: ".$OTP;
                        $url = "http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_post/";
                        $cPhone = $user[4];
                        $data = array('Phone' => $cPhone, 'Content' => $content, 'ApiKey' => ApiKey,'SecretKey' => SecretKey, 'IsUnicode' => '0', 'Brandname' => 'Verify','SmsType' => '2');

                        // use key 'http' even if you send the request to https://...
                        $options = array(
                            'http' => array(
                                'header'  => "Content-type: application/x-www-form-urlencoded\r\n",
                                'method'  => 'POST',
                                'content' => http_build_query($data)
                            )
                        );
                        $context  = stream_context_create($options);
                        $result = file_get_contents($url, false, $context);
                        if ($result === FALSE) 
                        {
                            $view_data['errors'][] = "Lỗi gửi tin nhắn OTP!";
                        }
                        else 
                        {

                             $_SESSION['OTPForAdmin'] = $OTP;
                            header('Location:'.base_url_admin."/account/enterotp&username=".$_POST['userlogin']);
                            exit();
                        }
                    }
                    else 
                    {

                        if($_COOKIE['OTPForAdmin'] == $user[7])
                        {
                            $_SESSION['userlogged'] = $user;
                            header('Location:'.base_url_admin);
                            exit();
                        }
                       
                    }
                }
                else 
                {

                    $view_data['errors'][] = "Sai tài khoản hoặc mật khẩu!";
                }
            }
             catch(Error  $e) {
                
                $view_data['errors'][] =  "Xảy ra lỗi khi xử lý! Hãy thử lại.";
                $trace = $e->getTrace();
                $errorMessage =  $e->getMessage().' in '.$e->getFile().' on line '.$e->getLine().' called from '.$trace[0]['file'].' on line '.$trace[0]['line'];
                $myfile = fopen(SITE_PATH."/log/errors.txt", "w") or die("Unable to open file!");
                fwrite($myfile, $errorMessage."\n");
                fclose($myfile);
            }
        }
        else 
        {
            $view_data['errors'][] = "Bạn cần nhập đầy đủ tài khoản và mật khẩu!";
        }
    }
	include_once '../admin/view/account/login.php';
?>
