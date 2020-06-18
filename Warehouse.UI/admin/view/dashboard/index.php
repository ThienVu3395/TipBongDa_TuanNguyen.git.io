<section class="content-header">
  	<h1>
    	<?= $view_data['title'] ?>
    	<small>Giúp bạn có cái nhìn tổng quát về website</small>
  	</h1>
  	<ol class="breadcrumb">
    	<li class="active"><i class="fa fa-dashboard"></i> <?= $view_data['title'] ?></li>
 	 </ol>
</section>
<section class="content">
	<div class="row">
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><?= $view_data['CountCardChargeToday'] ?></h3>

              <p>Lượt Nạp Thẻ Hôm Nay</p>
            </div>
            <div class="icon">
              <i class="ion ion-card"></i>
            </div>
            <a href="<?= base_url_admin ?>/dashboard/GetCardChargeToday" class="small-box-footer">Xem Danh Sách<i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><?=$view_data['CountUsers']?></h3>

              <p>Tổng Thành Viên</p>
            </div>
            <div class="icon">
              <i class="ion ion-ios-people-outline"></i>
            </div>
            <a href="<?= base_url_admin ?>/user" class="small-box-footer">Xem Danh Sách<i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3><?= $view_data['CountUsersOnline'] ?></h3>

              <p>Thành Viên Đang Online</p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="<?=base_url_admin?>/user/useronline" class="small-box-footer">Xem Danh Sách <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-red">
            <div class="inner">
              <h3><?= $view_data['GetTotalnExtPoint1'] ?></h3>

              <p>Tổng Xu Đang Có</p>
            </div>
            <div class="icon">
              <i class="ion ion-social-usd"></i>
            </div>
            <a href="<?=base_url_admin?>/user/userHasExtpoint1" class="small-box-footer">Xem Thành Viên Có Xu<i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
  </div>
  <div class="row">
    <div class="col-md-6">
       <!-- BAR CHART -->
          <div class="box box-success">
            <div class="box-header with-border">
              <h3 class="box-title">Doanh Thu Thẻ Từng Tháng</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <div class="box-body">
              <div class="chart">
                <canvas id="barChart" style="height:400px"></canvas>
              </div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
    </div>
    <div class="col-md-6">
      <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Top Nạp Thẻ</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
               <table class="table table-bordered">
                  <tbody>
                  <tr>
                      <th style="width: 10px">Top</th>
                        <th>Tài khoản</th>
                        <th>Tổng tiền nạp</th>
                  </tr>
                     <?php $i = 1; foreach($view_data['topnapthe'] as $item) { ?>
                  <tr>
                        <td><span  class="badge <?= $i == 1 ? "bg-red":"bg-green" ?> "><?= $i++ ?></span></td>
                        <td><?= $item[0] ?></td>
                        <td>
                            <b><?= str_replace(',','.',number_format($item[1])) ?> đồng</b>
                        </td>
                  </tr>
                    <?php } ?>
                  </tbody>
                  <tfoot>
                    <tr>
                      <td colspan="3" class="text-center">
                        <a href="<?= base_url_admin ?>/dashboard/topnapthe" class="btn btn-primary">Xem thêm</a>
                      </td>
                    </tr>
                  </tfoot>
               </table> 
            </div>
            <!-- /.box-body -->
        </div>        
    </div>
  </div>
  <div class="row">
    <div class="col-sm-6">
        <div class="box box-info">
          <div class="box-header with-border">
            <h3 class="box-title">Lịch Sử Nạp Thẻ Gần Đây</h3>

            <div class="box-tools pull-right">
              <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
              </button>
              <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
            </div>
          </div>
          <!-- /.box-header -->
          <div class="box-body">
            <div class="table-responsive">
              <table class="table no-margin">
                <thead>
                <tr>
                  <!-- <th>Transaction ID</th> -->
                  <th>Tài Khoản</th>
                  <th>Kết Quả</th>
                  <!-- <th>Kết quả</th> -->
                  <th>Thời Gian</th>
                </tr>
                </thead>
                <tbody>
                  <?php foreach($view_data['recentCharge'] as $item) { ?>
                  <tr>
                    <!-- <td><a href="javascript:void(0)"><?= $item['trans_id'] ?></a></td> -->
                    <td><?= $item['cAccName'] ?></td>
                    <td>
                      <?php if((int)$item['Status'] == 100 || $item['Status'] == 200 || $item['Status'] == 201) { ?>
                      <span class="label label-success"><?= $item['Status'] ?></span>
                      <?php } else { ?>
                      <span class="label label-danger"><?= $item['Status'] ?></span>  
                      <?php } ?>
                    </td>
                    <!-- <td><?= $item['Msg'] ?></td> -->
                    <td>
                       <?= date_format($item['DateRequest'],'d/m/Y h:i A') ?>
                    </td>
                  </tr>
                <?php } ?>
                </tbody>
              </table>
            </div>
            <!-- /.table-responsive -->
          </div>
          <!-- /.box-body -->
        </div>
    </div>
    <div class="col-md-6">
      <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Top Nạp Xu</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
               <table class="table table-bordered">
                  <tbody>
                    <tr>
                          <th style="width: 10px">Top</th>
                          <th>Tài Khoản</th>
                          <th>Tổng Xu Nạp</th>
                    </tr>
                  <?php $i = 1; foreach($view_data['topnapxu'] as $item) { ?>
                    <tr>
                          <td><span  class="badge <?= $i == 1 ? "bg-red":"bg-green" ?> "><?= $i++ ?></span></td>
                          <td><?= $item[0] ?></td>
                          <td>
                              <b><?= $item[1] ?> xu</b>
                          </td>
                    </tr>
                  <?php } ?>
                  </tbody>
                  <tfoot>
                    <tr>
                      <td colspan="3" class="text-center">
                        <a href="<?= base_url_admin ?>/dashboard/topnapxu" class="btn btn-primary">Xem thêm</a>
                      </td>
                    </tr>
                  </tfoot>
               </table> 
            </div>
            <!-- /.box-body -->
        </div>        
    </div>
  </div>  
</section>