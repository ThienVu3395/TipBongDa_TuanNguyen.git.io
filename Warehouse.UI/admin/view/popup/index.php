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
                <form method="post">
                    <?php 
                      $token = NoCSRF::generate( 'csrf_token' );
                    ?>
                    <input type="hidden" name="csrf_token" value="<?= $token ?>" /> 
                    
                    <div class="form-horizontal">
                        <?php include_once 'view/shared/_errors.php'; ?>
                      
                        <div class="form-group">
                            <label class="control-label col-md-1">Nội Dung</label>
                            <div class="col-md-10">
                                <textarea class="form-control" name="Content" id="Content"><?=$view_data['model']['Content']?></textarea>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-1">Hiển Thị</label>
                            <div class="col-md-3">
                                <input type="checkbox" name="Display" <?= $view_data['model']['Display'] == true ? "checked=''" : "" ?>  />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-1">Timeout</label>
                            <div class="col-md-3">
                                <input class="form-control" required="" min="0" name="Timeout" step="1" type="number" value="<?= (int)$view_data['model']['Timeout'] / 1000 ?>">
                            </div>
                            <div class="col-md-1">
                                <label>giây</label>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-offset-3 col-md-8">
                                <input type="submit" value="Sửa" class="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </form>

                <h4>Preview</h4>
                <hr />
                <div class="md-content" style="background:#fff;color:#000">
                    <h3>Bảng Tin</h3>
                    <button class="md-close" style="position:absolute;
                            right: 10px;
                            top:0px;">
                        X
                    </button>
                    <div>
                        <?= $view_data['model']['Content'] ?>
                        <button class="md-close">Đóng</button>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
         </div>
      </div>
      <!-- /.col -->
   </div>
   <!-- /.row -->
</section>

