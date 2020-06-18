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
        <div class="col-xs-6 col-md-offset-3">
              <div class="box">
                  <div class="box-body">
                      <div class="col-md-12">
                          <form  method="post">     
                              <input type="hidden" name="username" value="<?= $_GET['username'] ?>" />
                              <div class="form-group">
                                <label class="control-label">Nhập mã OTP đã được gửi đến điện thoại của bạn</label>
                                <input type="text" name="otp" class="form-control" required="" placeholder="OTP" />
                              </div>
                              <div class="form-group">
                                <button type="submit" class="btn btn-primary">Xác nhận</button>
                              </div>
                              
                              <?php 
                                $token = NoCSRF::generate( 'csrf_token' );
                              ?>
                              <input type="hidden" name="csrf_token" value="<?= $token ?>"> 
                              <?php include_once '../view/shared/_errors.php'; ?>

                          </form>
                      </div>
                  </div>
                  <!-- /.box-body -->
              </div>
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->
</section>
