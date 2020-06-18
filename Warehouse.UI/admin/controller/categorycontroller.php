<?php 
  include '../model/class.category.php';
  include '../model/nocsrf.php';
  $categoryModel = new Category();
  
 
  switch($action)
  {
  	case "index":
  	{
  		$view_data['title'] = "Chuyên mục bài viết";
  		$view_data['view_name'] = "category/index.php";	
  		$view_data['section_styles'] = "category/style_index.php";
  		$view_data['section_scripts'] = "category/script_index.php";
  		$view_data['model'] = $categoryModel->GetListWithIndex();
  		break;
  	}
    case "create":
    {
      $view_data['title'] = "Tạo mới chuyên mục";
      $view_data['view_name'] = "category/create.php";
      $view_data['section_scripts'] = "category/script_form.php";
      if(isset($_POST['name']))
      {
        try 
        {
          $name = $_POST['name'];
          $status = isset($_POST['status']) ? 1 : 0;
          $sort_order = (int)$_POST['sort_order'];
          $alias = $_POST['alias'];
          $view_data['errors'] = $categoryModel->GetErrorsMessage($name, $sort_order, $status, $alias);
          NoCSRF::check( 'csrf_token', $_POST, true, 60*10, false );
          if(count($view_data['errors']) == 0)
          {
            $result = $categoryModel->Add($name, $sort_order, $status, $alias);
            if($result)
            {
               header("Location: ".base_url_admin."/category"); 
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
      $view_data['title'] = "Sửa chuyên mục";
      $view_data['view_name'] = "category/edit.php";
      $view_data['section_scripts'] = "category/script_form.php";
      if(isset($_POST['name']))
      {
        try 
        {
          

          $id = (int)$_GET['id'];
          $name = $_POST['name'];
          $status = isset($_POST['status']) ? 1 : 0;
          $sort_order = (int)$_POST['sort_order'];
          $alias = $_POST['alias'];
          $view_data['errors'] = $categoryModel->GetErrorsMessage($name, $sort_order, $status, $alias);
          NoCSRF::check( 'csrf_token', $_POST, true, 60*10, false );
          if(count($view_data['errors']) == 0)
          {
            $result = $categoryModel->Edit($id, $name, $sort_order, $status, $alias);
            if($result)
            {
               header("Location: ".base_url_admin."/category"); 
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
        $view_data['model'] = $categoryModel->GetById($id);
        if($view_data['model'] == null)
        {
          header("Location: ".base_url."pages/404.html");
        }
        $name = $view_data['model'][1];
        $status = $view_data['model'][3];
        $sort_order = $view_data['model'][2];
        $alias = $view_data['model'][6];
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
  				$result = $categoryModel->Delete($id);
  				if($result)
  				 	header('Location:'.base_url_admin."/category");
  				else 
  					$view_data['errors'][] = "Đã có lỗi xảy ra";
  			}
  			catch ( Exception $e )
        {
            $view_data['errors'][] = $e->getMessage();
        }  
  		}
  		$view_data['title'] = "Chuyên mục bài viết";
  		$view_data['view_name'] = "category/index.php";	
  		$view_data['section_styles'] = "category/style_index.php";
  		$view_data['section_scripts'] = "category/script_index.php";
  		$view_data['model'] = $categoryModel->GetList();
  		break;
  	}
    
  }

?>