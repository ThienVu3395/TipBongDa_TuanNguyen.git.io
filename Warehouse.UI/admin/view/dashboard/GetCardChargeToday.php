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
                         <th>Tài khoản</th>
                         <th>Seri</th>
                         <th>Code</th>
                         <th>Status</th>
                         <th>Message</th>
                         <th>Transaction Id</th>
                         <th>Ngày nạp</th>
                         <th>Tiền nạp</th>
                      </tr>
                  </thead>
                  <tbody>
                     <?php foreach($view_data['model'] as $item) { ?>
                        <tr>
                           <td>
                              <?= $item["cAccName"] ?>
                           </td>
                           <td>
                              <?= $item["Seri"] ?>
                           </td>
                           <td>
                              <?= $item["Code"] ?>
                           </td>  
                           <td>
                              <?php if($item['Status'] == 200 || $item['Status'] == 201) { ?>
                                <label class='label label-success'><?= $item["Status"] ?></label>
                              <?php } else  {?>
                                <label class='label label-danger'><?= $item["Status"] ?></label>
                              <?php }?>
                           </td> 
                           <td>
                                 <?= $item["Msg"] ?>
                            </td>    
                            <td>
                                 <?= $item["trans_id"] ?>
                            </td>                                              
                            <td>
                               <?= $item["DateRequest"] != null ? date_format($item["DateRequest"], "d/m/Y h:M:t A") : "" ?>
                            </td>
                            <td>
                                 <?= str_replace(",", ".", number_format($item["Amount"])) ?> VNĐ
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