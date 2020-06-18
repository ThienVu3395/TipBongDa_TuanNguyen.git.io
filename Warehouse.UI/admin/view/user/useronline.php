<section class="content-header">
   <h1>
      <?= $view_data['title'] ?>
   </h1>
   <ol class="breadcrumb">
      <li><a href="<?=base_url_admin?>"><i class="fa fa-dashboard"></i> Bảng điều khiển</a></li>
      <li class="active"><?= $view_data['title'] ?></li>
   </ol>
</section>
<section class="content">
   <div class="row">
      <div class="col-xs-12">
        <div class="box">
            <div id="box">
              <div class="box-body clearfix">
                 <div class="table-responsive">
                    <table id="tblData" class="table table-bordered" method="post">
                      <thead>
                         <tr>
                            <th class="text-center">STT</th>
                            <th>Tài khoản</th>
                            <th>Điện thoại</th>
                            <th>Xu (nExtPoint1)</th>
                            <th>Trạng thái</th>
                            <th>Giờ chơi còn lại</th>
                            <th>Ngày hết hạn</th>
                            <th>Bị khóa?</th>
                            <th class="text-center">Thao tác</th>
                         </tr>
                      </thead>
                      <tbody>
                         <?php $i = 1; foreach($view_data['model'] as $item) { ?>
                         <tr>
                            <td class="text-center"><?= $i++ ?></td>
                            <td>
                              <?= $item["cAccName"] ?>  
                            </td>
                             <td>
                              <?= $item["cPhone"] ?>  
                            </td>
                            <td>
                                <?= $item["nExtPoint1"] ?>
                            </td>
                            <td>
                                <?php if((int)$item['iClientID'] != 0) { ?>
                                   <i class="fa fa-circle text-success"></i> Online
                                <?php } else { ?>
                                  <i class="fa fa-circle text-danger"></i> Offline
                                <?php } ?>
                            </td>  
                            <td>
                                <?= number_format((float)$item["iLeftSecond"] / 3600, 2, '.', '') ?> giờ
                            </td> 
                            <td>
                                <?= $item["dEndDate"] != null ? date_format($item["dEndDate"], "d/m/Y") : "" ?>
                            </td>                                                
                            <td class="text-center">
                               <?php if ($item["bIsBanned"] == 0) { ?>
                               
                               <label class="label label-success">Đang mở</label>
                               <?php } else { ?>
                               <label class="label label-danger">Bị khóa</label>
                               <?php } ?>
                            </td>
                            <td class="text-center col-sm-3">
                              <a class="btn bg-blue btn-xs" href="<?= base_url_admin ?>/user/details&cAccName=<?= $item["cAccName"] ?>"><i class="fa fa-info"></i> Thông tin</a>
                              <a class="btn bg-purple btn-xs" href="<?= base_url_admin ?>/user/historyCharge&cAccName=<?= $item["cAccName"] ?>"><i class="fa fa-clock-o"></i> Lịch sử nạp thẻ</a>
                            </td>
                         </tr>
                         <?php } ?>
                      </tbody>
                    </table>
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
