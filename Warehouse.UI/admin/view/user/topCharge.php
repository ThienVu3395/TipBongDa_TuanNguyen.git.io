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
      <div class="col-md-6 col-md-offset-3 margin-top-10">
         <div class="box">
            <div id="box">
               <div class="box-body clearfix margin-top-10">
                    <div class="table-responsive" id="divTableData">
                       <table class="table table-bordered">
                          <tbody>
                         	<tr>
                            	<th style="width: 10px">Top</th>
                                <th>Tài khoản</th>
                                <th>Tổng tiền nạp</th>
                         	</tr>
                             <?php $i = 1; foreach($view_data['model'] as $item) { ?>
                         	<tr>
                                <td><span  class="badge <?= $i == 1 ? "bg-red":"bg-green" ?> "><?= $i++ ?></span></td>
                                <td><?= $item[0] ?></td>
                                <td>
                                  	<b><i><?= str_replace(',','.',number_format($item[1])) ?> đồng</i></b>
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