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
               <?php include_once 'view/shared/_errors.php'; ?>
               <?php 
                  $token = NoCSRF::generate( 'csrf_token' );
               ?>
               <div class="table-responsive">
                  <table id="tblData" class="table table-bordered" method="post">
                  <thead>
                     <tr>
                        <th class="text-center">Mã số</th>
                        <th>Tiêu đề</th>
                        <th>Thể loại</th>
                        <th class="text-center">Trạng thái</th>
                        <th class="text-center">Ưu tiên</th>
                        <th>Bí danh</th>
                        <th class="text-center col-sm-3">Thao tác</th>
                     </tr>
                  </thead>
                  <tbody>
                     <?php foreach($view_data['model'] as $item) { ?>
                     <tr>
                        <td class="text-center"><?= $item[0] ?></td>
                        <td>
                           <a target="_blank" href="<?=base_url?>/bai-viet/<?= $item[5] ?>.html"><?= $item[1] ?></a>
                        </td>
                        <td>
                           <?= $item[10] ?>
                        </td>
                        <td class="text-center">
                           <?php if ($item[6] == 1) { ?>
                           
                           <label class="label label-success">Hiển thị</label>
                           <?php } else { ?>
                           <label class="label label-danger">Ẩn</label>
                           <?php } ?>
                        </td>
                        <td class="text-center">
                           <?php if ($item[9] == 1) { ?>
                           <label class="label label-success">Ưu tiên</label>
                           <?php } else { ?>
                           <label class="label label-default">Mặc định</label>
                           <?php } ?>
                        </td>
                        <td>
                           <?= $item[5] ?>
                        </td>
                        <td class="text-center">
                           <a class="btn bg-blue btn-xs" href="<?= base_url_admin ?>/article/edit/<?= $item[0] ?>"><i class="fa fa-edit"></i> Sửa</a>
                          
                           <form class="hidden" id="frmDelete_<?= $item[0] ?>" action="<?= base_url_admin ?>/article/delete" method="post">
                              <input type="hidden" name="csrf_token" value="<?= $token ?>" />
                              <input type="hidden" name="id" value="<?= $item[0] ?>" />
                           </form>
                           <button class="btn bg-red btn-xs" onclick="if(confirm('Xác nhận xóa bài viết này?')) { document.getElementById('frmDelete_<?= $item[0] ?>').submit();  }"><i class="fa fa-remove"></i> Xóa</button>
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
      <!-- /.col -->
   </div>
   <!-- /.row -->
</section>