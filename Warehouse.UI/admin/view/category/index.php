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
               <p>
                  <a class="btn btn-success btn-xs" href="<?=base_url_admin?>/category/create">Thêm Mới</a>
               </p>
                <?php 
                             $token = NoCSRF::generate( 'csrf_token' );
                           ?>
               <table id="tblData" class="table table-bordered" method="post">
                  <thead>
                     <tr>
                        <th>STT</th>
                        <th>Tên chuyên mục</th>
                        <th class="text-center">Trạng thái</th>
                        <th>Bí danh</th>
                        <th>Ngày tạo</th>
                        <th class="text-center">Thao tác</th>
                     </tr>
                  </thead>
                  <tbody>
                     <?php foreach($view_data['model'] as $item) { ?>
                     <tr>
                        <td class="text-center">
                           <?= $item[2] ?>
                        </td>
                        <td>
                           <?= $item[1] ?>
                        </td>
                        <td class="text-center">
                           <?php if ($item[3] == 1) { ?>
                           <label class="label label-success">Hiển thị</label>
                           <?php } else { ?>
                           <label class="label label-danger">Ẩn</label>
                           <?php } ?>
                        </td>
                        <td>
                           <?= $item[6] ?>
                        </td>
                        <td>
                           <?= date_format($item[4],'d-m-Y') ?>
                        </td>
                        <td class="text-center">
                          
                           <a class="btn bg-blue btn-xs" href="<?= base_url_admin ?>/category/edit/<?= $item[0] ?>"><i class="fa fa-edit"></i> Sửa</a>
                          
                           <form class="hidden" id="frmDelete_<?= $item[0] ?>" action="<?= base_url_admin ?>/category/delete" method="post">
                              <input type="hidden" name="csrf_token" value="<?= $token ?>" />
                              <input type="hidden" name="id" value="<?= $item[0] ?>" />
                           </form>
                           <button class="btn bg-red btn-xs" onclick="if(confirm('Xác nhận xóa chuyên mục <?= $item[1] ?> ?')) { document.getElementById('frmDelete_<?= $item[0] ?>').submit();  }"><i class="fa fa-remove"></i> Xóa</button>
                        </td>
                     </tr>
                     <?php } ?>
                  </tbody>
               </table>
            </div>
            <!-- /.box-body -->
         </div>
      </div>
      <!-- /.col -->
   </div>
   <!-- /.row -->
</section>