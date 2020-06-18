<?php

include '../model/class.logchange.php';
include '../model/nocsrf.php';
$logChangeModel = new LogChange();

switch ($action) {
    case "index": {
            $view_data['title'] = "Lịch Sử Thay Đổi Thông Tin Các Thành Viên";
            $view_data['view_name'] = "logchange/index.php";
            $view_data['section_styles'] = "logchange/style_index.php";
            $view_data['section_scripts'] = "logchange/script_index.php";
            $view_data['model'] = $logChangeModel->GetAll();
            break;
        }
}

