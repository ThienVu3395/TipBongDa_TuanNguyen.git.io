<div class="box-tools1" id="box-tools-custom">
    <ul class="pagination no-margin pull-right">
      <li><a href="<?= base_url_admin."/user/paging&page=1" ?>"><<</a></li>
      <?php if($view_data['pageCurrent'] - 1 >= 1) { ?>
        <li><a href="<?= base_url_admin."/user/paging&page=".($view_data['pageCurrent'] - 1) ?>"><</a></li>
      <?php } ?>
      <?php for($i = $view_data['pageCurrent'] - 5 >= 1 ? $view_data['pageCurrent'] - 5   : 1 ; $i <= ($view_data['pageCurrent'] + 5 <= $view_data['totalPage'] ?  $view_data['pageCurrent'] + 5  : $view_data['totalPage']) ; $i++) { ?>
      <li <?= $i == $view_data['pageCurrent'] ? "class='active'" : "" ?>><a href="<?= base_url_admin."/user/paging&page=".$i ?>" ><?= $i ?></a></li>
    <?php } ?>
    <?php if($view_data['pageCurrent'] + 1 <= $view_data['totalPage']) { ?>
        <li><a href="<?= base_url_admin."/user/paging&page=".($view_data['pageCurrent'] + 1) ?>">></a></li>
      <?php } ?>
      <li><a href="<?= base_url_admin."/user/paging&page=".$view_data['totalPage'] ?>">>></a></li>
    </ul>
</div>
<div class="box-body clearfix">
   <?php include_once 'view/shared/_errors.php'; ?>
   <?php 
      $token = NoCSRF::generate( 'csrf_token' );
   ?>
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
           <?php $j = 1; $i = 20 * ($view_data['pageCurrent'] - 1) + 1; foreach($view_data['model'] as $item) { ?>
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
                <!-- <a class="btn bg-blue btn-xs" data-toggle="modal" href="#modal-thongtin-<?= $j ?>"><i class="fa fa-info"></i> Thông tin</a> -->
                <a class="btn bg-blue btn-xs"  href="<?=base_url_admin?>/user/details&cAccName=<?= $item["cAccName"]  ?>"><i class="fa fa-info"></i> Thông tin</a>
                <a class="btn bg-purple btn-xs" href="<?= base_url_admin ?>/user/historyCharge&cAccName=<?= $item["cAccName"] ?>"><i class="fa fa-clock-o"></i> Lịch sử nạp thẻ</a>
              </td>
           </tr>
           <?php $j++; } ?>
        </tbody>
      </table>
   </div>
</div>
<!-- /.box-body -->
<div class="box-footer clearfix">
  <ul class="pagination no-margin pull-right">
    <li><a href="<?= base_url_admin."/user/paging&page=1" ?>"><<</a></li>
    <?php if($view_data['pageCurrent'] - 1 >= 1) { ?>
      <li><a href="<?= base_url_admin."/user/paging&page=".($view_data['pageCurrent'] - 1) ?>"><</a></li>
    <?php } ?>
    <?php for($i = $view_data['pageCurrent'] - 5 >= 1 ? $view_data['pageCurrent'] - 5   : 1 ; $i <= ($view_data['pageCurrent'] + 5 <= $view_data['totalPage'] ?  $view_data['pageCurrent'] + 5  : $view_data['totalPage']) ; $i++) { ?>
    <li <?= $i == $view_data['pageCurrent'] ? "class='active'" : "" ?>><a href="<?= base_url_admin."/user/paging&page=".$i ?>" ><?= $i ?></a></li>
  <?php } ?>
    <?php if($view_data['pageCurrent'] + 1 <= $view_data['totalPage']) { ?>
      <li><a href="<?= base_url_admin."/user/paging&page=".($view_data['pageCurrent'] + 1) ?>">></a></li>
    <?php } ?>
    <li><a href="<?= base_url_admin."/user/paging&page=".$view_data['totalPage'] ?>">>></a></li>
  </ul>
</div>      
<script>

    $(function () {
        $('#tblData').DataTable({
            columnDefs: [{ orderable: false, targets: [8] }],
            order: [[0, 'asc']],
            paging:false,
            searching: false,
            lengthChange: true,
            retrieve: true,
        });
    });
</script>