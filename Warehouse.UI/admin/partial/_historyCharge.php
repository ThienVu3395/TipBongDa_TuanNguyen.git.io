
<div class="box-tools1" id="box-tools-custom">
  <ul class="pagination no-margin pull-right">
    <li><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=1"."&cAccName=".$view_data['cAccName']?>"><<</a></li>
    <?php if($view_data['pageCurrent'] - 1 >= 1) { ?>
      <li><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=".($view_data['pageCurrent'] - 1)."&cAccName=".$view_data['cAccName'] ?>"><</a></li>
    <?php } ?>
    <?php for($i = $view_data['pageCurrent'] - 5 >= 1 ? $view_data['pageCurrent'] - 5   : 1 ; $i <= ($view_data['pageCurrent'] + 5 <= $view_data['totalPage'] ?  $view_data['pageCurrent'] + 5  : $view_data['totalPage']) ; $i++) { ?>
    <li <?= $i == $view_data['pageCurrent'] ? "class='active'" : "" ?>><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=".$i."&cAccName=".$view_data['cAccName'] ?>" ><?= $i ?></a></li>
  <?php } ?>
    <?php if($view_data['pageCurrent'] + 1 <= $view_data['totalPage']) { ?>
      <li><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=".($view_data['pageCurrent'] + 1)."&cAccName=".$view_data['cAccName'] ?>">></a></li>
    <?php } ?>
    <li><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=".$view_data['totalPage']."&cAccName=".$view_data['cAccName']?>">>></a></li>
  </ul>
</div>
<div class="box-body clearfix">
   <div class="table-responsive">
      <table id="tblData" class="table table-bordered table-tripped">
          <thead>
             <tr>
                <th class="hidden">Id</th>
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
                <td class="hidden"><?= $item['Id'] ?></td>
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
                    <?php if($item['Status'] == 201 || $item['Status'] == 100) { ?>
                   	  <label class="label label-success"><?= $item["Status"] ?></label>
                    <?php } else {  ?>
                      <label class="label label-danger"><?= $item["Status"] ?></label>
                    <?php }?>
                </td> 
                <td>
                   	<?= $item["Msg"] ?>
                </td>    
                <td>
                   	<?= $item["trans_id"] ?>
                </td>                                              
                <td>
                   <?= $item["DateRequest"] != null ? date_format($item["DateRequest"], "d/m/Y h:i:s A") : "" ?>
                </td>
                <td>
                   	<?= number_format($item["Amount"]) ?> VNĐ
                </td>
             </tr>
             <?php } ?>
          </tbody>
      </table>
      <!-- <br/> -->
    <!--   <div class="text-right">
         <h5>Tổng tiền nạp: <?= number_format($sum) ?> VNĐ</h5>
      </div> -->
   </div>
</div>  
<div class="box-footer clearfix">
  <ul class="pagination no-margin pull-right">
    <li><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=1"."&cAccName=".$view_data['cAccName']?>"><<</a></li>
    <?php if($view_data['pageCurrent'] - 1 >= 1) { ?>
      <li><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=".($view_data['pageCurrent'] - 1)."&cAccName=".$view_data['cAccName'] ?>"><</a></li>
    <?php } ?>
    <?php for($i = $view_data['pageCurrent'] - 5 >= 1 ? $view_data['pageCurrent'] - 5   : 1 ; $i <= ($view_data['pageCurrent'] + 5 <= $view_data['totalPage'] ?  $view_data['pageCurrent'] + 5  : $view_data['totalPage']) ; $i++) { ?>
    <li <?= $i == $view_data['pageCurrent'] ? "class='active'" : "" ?>><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=".$i."&cAccName=".$view_data['cAccName'] ?>" ><?= $i ?></a></li>
  <?php } ?>
    <?php if($view_data['pageCurrent'] + 1 <= $view_data['totalPage']) { ?>
      <li><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=".($view_data['pageCurrent'] + 1)."&cAccName=".$view_data['cAccName'] ?>">></a></li>
    <?php } ?>
    <li><a href="<?= base_url_admin."/user/pagingHistoryCharge&page=".$view_data['totalPage']."&cAccName=".$view_data['cAccName']?>">>></a></li>
  </ul>
</div>   
<script>
    $(function () {
        $('#tblData').DataTable({
            order: [[0, 'desc']],
            paging:false,
            searching: true,
            retrieve: true,
        });
        
    });
</script>