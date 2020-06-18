<?php 
  include '../model/class.article.php';
  include '../model/class.category.php';
  include '../model/nocsrf.php';
  $articleModel = new Article();
  $categoryModel = new Category();
 
  switch($action)
  {
  	case "index":
  	{
  		$view_data['title'] = "Bài viết";
  		$view_data['view_name'] = "article/index.php";	
  		$view_data['section_styles'] = "article/style_index.php";
  		$view_data['section_scripts'] = "article/script_index.php";
  		$view_data['model'] = $articleModel->GetList();
  		break;
  	}
    case "create":
    {
      $view_data['title'] = "Tạo mới bài viết";
      $view_data['view_name'] = "article/create.php";
      $view_data['categories'] = $categoryModel->GetList(1);
      $view_data['section_scripts'] = "article/script_form.php";
      if(isset($_POST['title']))
      {
        try 
        {
          
          $title = $_POST['title'];
          $cat_id = (int)$_POST['cat_id'];
          $content = $_POST['content'];
          $status = isset($_POST['status']) ? 1 : 0;
          $isFirst = isset($_POST['isFirst']) ? 1 : 0;
          $alias = strip_tags($_POST['alias']); 
          $view_data['errors'] = $articleModel->GetErrorsMessage($title, $cat_id, $alias, $status, true, true);
          NoCSRF::check( 'csrf_token', $_POST, true, 60*10, false );
          if(count($view_data['errors']) == 0)
          {
            $result = $articleModel->Add($title, $cat_id, $content, $alias, $status, $isFirst);
            if($result)
            {
               header("Location: ".base_url_admin."/article"); 
            }
            else 
            {
                $view_data['errors'][] = "Đã có lỗi xảy ra";
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
      $view_data['title'] = "Sửa bài viết";
      $view_data['view_name'] = "article/edit.php";
      $view_data['section_scripts'] = "article/script_form.php";
      $view_data['categories'] = $categoryModel->GetList(1);
      if(isset($_POST['title']))
      {
        try 
        {
          
          $id = (int)$_GET['id'];
          $title = $_POST['title'];
          $title_old = $_POST['title_old'];
          $cat_id = (int)$_POST['cat_id'];
          $content = $_POST['content'];
          $status = isset($_POST['status']) ? 1 : 0;
          $isFirst = isset($_POST['isFirst']) ? 1 : 0;
          $alias = $_POST['alias'];
          $alias_old = $_POST['alias_old'];
          $view_data['errors'] = $articleModel->GetErrorsMessage($title, $cat_id, $alias, $status, $title_old != $title, $alias_old != $alias);
          NoCSRF::check( 'csrf_token', $_POST, true, 60*10, false );
          if(count($view_data['errors']) == 0)
          {
            $result = $articleModel->Edit($id, $title, $cat_id, $content, $alias, $status, $isFirst);
            if($result)
            {
               header("Location: ".base_url_admin."/article"); 
            }
            else 
            {
                $view_data['errors'][] = "Đã có lỗi xảy ra";
            }
          }
        }
        catch(Exception $ex)
        {
          $view_data['errors'][] = $ex->getMessage();
        }
      } 
      else 
      {
        $id = (int)$_GET['id'];
        $view_data['model'] = $articleModel->GetById($id);
        if($view_data['model'] == null)
        {
          header("Location: ".base_url."pages/404");
        }
        $title = $view_data['model'][1];
        $cat_id = (int)$view_data['model'][2];
        $content = $view_data['model'][3];
        $status = isset($view_data['model'][6]) ? 1 : 0;
        $alias = $view_data['model'][5];
        $isFirst = $view_data['model'][9];
      }
      break;
    }
  	case "delete":
  	{
  		if(isset($_POST['id']) && isset($_POST['csrf_token']))
  		{
  			try 
  			{
  				NoCSRF::check( 'csrf_token', $_POST, true, 60*10, false );
  				$id = (int)$_POST['id'];
  				$result = $articleModel->Delete($id);
  				if($result)
  				 	header('Location:'.base_url_admin."/article");
  				else 
  					$view_data['errors'][] = "Đã có lỗi xảy ra";
  			}
  			catch ( Exception $e )
        {
            $view_data['errors'][] = $e->getMessage();
        }  
  		}
      $view_data['title'] = "Bài viết";
      $view_data['view_name'] = "article/index.php";  
      $view_data['section_styles'] = "article/style_index.php";
      $view_data['section_scripts'] = "article/script_index.php";
      $view_data['model'] = $articleModel->GetList();
  		break;
  	}
    
  }

?>