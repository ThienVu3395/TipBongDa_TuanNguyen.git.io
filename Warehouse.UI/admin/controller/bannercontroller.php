<?php 
  include '../model/class.banner.php';
  include '../model/nocsrf.php';
  
  $bannerModel = new Banner();
  
 
  switch($action)
  {
  	case "index":
  	{
  		$view_data['title'] = "Banner";
  		$view_data['view_name'] = "banner/index.php";	
  		$view_data['section_styles'] = "banner/style_index.php";
  		$view_data['section_scripts'] = "banner/script_index.php";
  		$view_data['model'] = $bannerModel->GetList();
  		break;
  	}
    case "create":
    {
      $view_data['title'] = "Tạo mới banner";
      $view_data['view_name'] = "banner/create.php";
      $view_data['section_scripts'] = "banner/script_form.php";
      if(count($_POST) > 0)
      {
        try 
        {
          $title = $_POST['title'];
          $status = isset($_POST['status']) ? 1 : 0;
          $sort_order = (int)$_POST['sort_order'];
          $image = $_POST['image'];
          
          if(count($view_data['errors']) == 0)
          {
            $result = $bannerModel->Add($title, $image, $sort_order, $status);
            if($result)
            {
               header("Location: ".base_url_admin."/banner"); 
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
      $view_data['title'] = "Sửa banner";
      $view_data['view_name'] = "banner/edit.php";
      $view_data['section_scripts'] = "banner/script_form.php";
      $id = (int)$_REQUEST['id'];
      $view_data['model'] = $bannerModel->GetById($id);
      $title = $view_data['model']['title'];
      $status = $view_data['model']['status'];
      $sort_order = $view_data['model']['sort_order'];
      if($view_data['model'] == null)
      {
        header("Location: ".base_url."pages/404.html");
      }
      if(count($_POST) > 0)
      {
        try 
        {
          $image = $view_data['model']['image'];
          if(isset($_POST['image']) && !empty($_POST['image']) )
          {
            $image = $_POST['image'];
          }
          $title = $_POST['title'];
          $status = isset($_POST['status']) ? 1 : 0;
          $sort_order = (int)$_POST['sort_order'];
          $view_data['errors'] = $bannerModel->GetErrorsMessage($image, $sort_order, $status);
          if(count($view_data['errors']) == 0)
          {
            $result = $bannerModel->Edit($id, $title, $image, $sort_order, $status);
            if($result)
            {
               header("Location: ".base_url_admin."/banner"); 
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
        $image = $view_data['model']['image'];
        $sort_order = (int)$view_data['model']['sort_order'];
        $status = (int)$view_data['model']['status'];
      }
      break;
    }
  	case "delete":
  	{
  		if(isset($_POST['id']))
  		{
  			try 
  			{
  				$id = (int)$_POST['id'];
  				$result = $bannerModel->Delete($id);
  				if($result)
  				 	header('Location:'.base_url_admin."/banner");
  				else 
  					$view_data['errors'][] = "Đã có lỗi xảy ra";
  			}
  			catch ( Exception $e )
        {
            $view_data['errors'][] = $e->getMessage();
        }  
  		}
  		$view_data['title'] = "Banner";
  		$view_data['view_name'] = "banner/index.php";	
  		$view_data['section_styles'] = "banner/style_index.php";
  		$view_data['section_scripts'] = "banner/script_index.php";
  		$view_data['model'] = $bannerModel->GetList();
  		break;
  	}
    
  }

?>