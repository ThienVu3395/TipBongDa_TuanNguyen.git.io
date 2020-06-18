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
               <table id="tblData" class="table table-bordered table-tripped">
                  <thead>
                      <tr>
                         <th class="text-center">Thứ Hạng</th>
                         <th>Tài Khoản</th>
                         <th>Tổng Xu Đã Nạp</th>
                      </tr>
                  </thead>
                  <tbody>
                     <?php $i = 1; foreach($view_data['model'] as $item) { ?>
                        <tr>
                           <td class="text-center">
                              <span  class="badge <?= $i == 1 ? "bg-red":"bg-green" ?> "><?= $i++ ?></span>
                           </td>
                           <td>
                              <?= $item[0] ?>
                           </td>
                           <td>
                              <b><?= $item[1] ?> xu</b>
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