<section class="content-header">
    <h1>
        <?= $view_data['title'] ?>
    </h1>
    <ol class="breadcrumb">
        <li><a href="<?= base_url_admin ?>"><i class="fa fa-dashboard"></i> Bảng điều khiển</a></li>
        <li class="active"><?= $view_data['title'] ?></li>
    </ol>
</section>
<section class="content">
    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div id="box">
                    <div class="box-body clearfix">
                        <div class="row margin-top-10">
                            <ul class="list-group">
                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyTransferCoin">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Chuyển Xu</span>
                                    </a>
                                </li>
        
                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyChangeData/nExtPoint2">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Đổi Giọt Máu</span>
                                    </a>
                                </li>

                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyChangeData/nExtPoint3">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Đổi Điểm OTP</span>
                                    </a>
                                </li>
<?php if($_SESSION['userlogged'][6] == 'tuannguyen' || $_SESSION['userlogged'][6] == 'letuan' || $_SESSION['userlogged'][6] == 'quocdungml2019') { ?>
                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyChangeData/Pass1">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Đổi Pass 1</span>
                                    </a>
                                </li>

                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyChangeData/Pass2">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Đổi Pass 2</span>
                                    </a>
                                </li>
<?php } ?>
                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyChangeData/cPhone">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Đổi Số Điện Thoại</span>
                                    </a>
                                </li>

                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyChangeData/iLeftSecond">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Đổi Giây Chơi Còn Lại</span>
                                    </a>
                                </li>

                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyChangeData/dEndDate">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Đổi Ngày Kết Thúc</span>
                                    </a>
                                </li>

                                <li class="list-group-item">
                                    <a href="<?=base_url_admin?>/historyChangeData/dBeginDate">
                                        <i class="fa fa-list"></i> <span>Lịch Sử Đổi Ngày Bắt Đầu</span>
                                    </a>
                                </li>
                            </ul>						
                        </div>
                    </div>
                    <!-- /.box-body -->         	
                </div>
            </div>
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->
</section>