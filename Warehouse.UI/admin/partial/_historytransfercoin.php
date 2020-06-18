<div class="table-responsive">
  	<table id="tblData" class="table table-bordered" method="post">
      	<thead>
         	<tr>
	            <th class="text-center">Số thứ tự</th>
	            <th>Tài khoản chuyển</th>
	            <th>Tài khoản nhận</th>
	            <th>Số Xu</th>
	            <th>Ngày Chuyển</th>
         	</tr>
      </thead>
      <tbody>
     	<?php $i = 1; foreach($view_data['model'] as $item) { ?>
         	<tr>
	            <td class="text-center"><?= $i++; ?></td>
	             <td>
	               <?= $item['From'] ?>
	            </td>
	            <td>
	               <?= $item['To'] ?>
	            </td>
	            <td>
	               <?= $item["Amount"]  ?> Xu
	            </td>
	            <td>
	            	<?= date_format($item["DateTransfer"], "d/m/Y") ?>
	            </td>
         	</tr>
     	<?php } ?>
      </tbody>
	</table>
</div>
<script>
    $('#tblData').DataTable();
</script>