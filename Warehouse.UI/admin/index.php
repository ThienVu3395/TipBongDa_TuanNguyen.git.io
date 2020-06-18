<?php
  session_start(); 
  require_once '../model/config.php';
  require_once '../model/functions.php';
  require_once '../model/connection.php';
  require_once '../model/class.database.php';
  $controller = $_GET['controller'];
  $action = $_GET['action'];
  $view_data = array();
  $view_data['errors'] = array();
  if((!isset($_SESSION['userlogged']) || $_SESSION['userlogged'] == null) && $action != "enterotp" )
  {
    header("Location: ".base_url."/loginpk2020");
  }

  require_once 'controller/'.$controller."controller.php";
  include_once 'view/shared/_layout.php';
  
  sqlsrv_close( $conn);

 ?>