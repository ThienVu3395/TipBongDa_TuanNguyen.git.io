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
               <div class="box-body">
                  <?php if(isset($_GET['messageSuccess'])) { ?>
                  <div class="alert alert-success">
                     <?= $_GET['messageSuccess'] ?>
                  </div>
                  <?php } ?>
                  <?php include_once 'view/shared/_errors.php'; ?>
                  <div class="col-xs-12">
                     <div class="input-group col-md-4 col-md-offset-3 pull-left">
                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                        <input data-urlSubmit="<?=base_url_admin?>/user/searchOnPageDetail" type="text" class="form-control" name="keyword" id="keyword" placeholder="Nhập tài khoản cần xem thông tin">
                     </div>
                     <div class="input-group col-md-2 pull-left">
                        &nbsp;
                        <button data-urlSubmit="<?=base_url_admin?>/user/searchOnPageDetail" type="button" id="btnSearch" class="btn btn-primary">Xem Thông Tin</button>
                     </div>
                  </div>
                  <div>&nbsp;</div>
                    <form method="post">
                        <?php 
                           $token = NoCSRF::generate( 'csrf_token' );
                           ?>
                        <input type="hidden" name="csrf_token" value="<?= $token ?>"> 
                      <div class="col-md-12" id="infoUser">
                        
                      </div>
                    </form>
               </div>
               <!-- /.box-body -->
            </div>
         </form>
      </div>
      <!-- /.col -->
   </div>
   <!-- /.row -->
</section>
<div class="modal fade" id="modal-change-nExtPoint">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form" action="<?=base_url_admin?>/user/changeBalance" method="post">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Số Xu Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="csrf_token" value="<?= $token ?>"> 
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <!-- text input -->
               <div class="form-group">
                  <label>Số xu hiện tại</label>
                  <input type="text" class="form-control" value="<?= $view_data['model']['nExtPoint1'] ?>" readonly="" />
               </div>
               <div class="form-group">
                  <label>Chọn thay đổi</label>
                  <select name="changeType" class="form-control" id="chonthaydoi" onchange="document.getElementById('lblNhapSoXu').innerText = 'Nhấp số xu ' + this.value">
                     <option value="Tăng lên">Tăng lên</option>
                     <option value="Giảm xuống">Giảm xuống</option>
                  </select>
               </div>
               <div class="form-group">
                  <label id="lblNhapSoXu">Nhập số xu</label>
                  <input min="0" step="1" type="number" name="valueChange" class="form-control" placeholder="" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->