<?php 
  include '../model/nocsrf.php';
  include '../model/class.question.php';
  $questionModel = new Question();
  switch($action)
  {
  	case "index":
  	{
  		$view_data['title'] = "Câu Hỏi Bảo Mật";
  		$view_data['view_name'] = "question/index.php";	
      $view_data['section_styles'] = "question/style_index.php";
      $view_data['section_scripts'] = "question/script_index.php";
      $view_data['model'] = $questionModel->GetList();
  		break;
  	}
    case "create":
    {
      $view_data['title'] = "Tạo Mới Câu Hỏi";
      $view_data['view_name'] = "question/create.php";
      if(count($_POST) > 0)
      {
        try 
        {
          $Id = $_POST['Id'];
          $Content = strip_tags($_POST['content']); 
          $Sort_Order = (int)$_POST['sort_order'];
          $view_data['errors'] = $questionModel->GetErrorsMessage($Id, $Content, $Sort_Order);
          NoCSRF::check('csrf_token', $_POST, true, 60*10, false);
          if(count($view_data['errors']) == 0)
          {
            $result = $questionModel->Add($Id, $Content, $Sort_Order);
            if($result)
            {
              header("Location: ".base_url_admin."/question");
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
    case "edit":
    {
      $view_data['title'] = "Sửa Câu Hỏi";
      $view_data['view_name'] = "question/edit.php";
      $id = $_REQUEST['id'];
      $view_data['model'] = $questionModel->GetById($id);  
      $content = strip_tags($view_data['model']['Content']); 
      $sort_order = (int)$view_data['model']['Sort_Order'];
      if(count($_POST) > 0)
      {
        try 
        {
          $content = $_POST['content'];
          $sort_order = (int)$_POST['sort_order'];
          $view_data['errors'] = $questionModel->GetErrorsMessage($id, $content, $sort_order);
          NoCSRF::check('csrf_token', $_POST, true, 60*10, false);
          if(count($view_data['errors']) == 0)
          {
            $result = $questionModel->Edit($id, $content, $sort_order);
            if($result)
            {
              header("Location: ".base_url_admin."/question");
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
  	case "delete":
  	{
      if(count($_POST) > 0)
      {
        try 
        {
          $id = $_POST['id'];
          NoCSRF::check('csrf_token', $_POST, true, 60*10, false);
          $result = $questionModel->Delete($id);
          if($result)
          {
            header("Location: ".base_url_admin."/question");
          }
        }
        catch(Exception $ex)
        {
          $view_data['errors'][] = $ex->getMessage();
        }  
      }
      $view_data['title'] = "Câu Hỏi Bảo Mật";
      $view_data['view_name'] = "question/index.php"; 
      $view_data['section_styles'] = "question/style_index.php";
      $view_data['section_scripts'] = "question/script_index.php";
      $view_data['model'] = $questionModel->GetList();
  		break;
  	}
    
  }

?>