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
                  <a class="btn btn-success btn-xs" href="<?=base_url_admin?>/banner/create">Thêm Mới</a>
               </p>
               <table id="tblData" class="table table-bordered table-middle" method="post">
                  <thead>
                     <tr>
                        <th>Số thứ tự</th>
                        <th class="text-center">Hình</th>
                        <th>Tiêu đề</th>
                        <th class="text-center">Trạng thái</th>
                        <th class="text-center">Thao tác</th>
                     </tr>
                  </thead>
                  <tbody>
                     <?php foreach($view_data['model'] as $item) { ?>
                     <tr>
                        <td class="text-center">
                           <?= $item['sort_order'] ?>
                        </td>
                        <td class="text-center">
                           <img src="<?= $item['image'] ?>" width="250" height="auto" />
                        </td>
                        <td><?= $item['title'] ?></td>
                        <td class="text-center">
                           <?php if ($item['status'] == 1) { ?>
                           <label class="label label-success">Hiển thị</label>
                           <?php } else { ?>
                           <label class="label label-danger">Ẩn</label>
                           <?php } ?>
                        </td>
                       
                        <td class="text-center">
                          
                           <a class="btn bg-blue btn-xs" href="<?= base_url_admin ?>/banner/edit/<?= $item['id'] ?>"><i class="fa fa-edit"></i> Sửa</a>
                          
                           <form class="hidden" id="frmDelete_<?= $item['id'] ?>" action="<?= base_url_admin ?>/banner/delete" method="post">
                              <input type="hidden" name="id" value="<?= $item['id'] ?>" />
                           </form>
                           <button class="btn bg-red btn-xs" onclick="if(confirm('Xác nhận xóa banner này ?')) { document.getElementById('frmDelete_<?= $item['id'] ?>').submit();  }"><i class="fa fa-remove"></i> Xóa</button>
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