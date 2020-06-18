
<table id="tblData" class="table table-bordered" >
	<thead>
	 	<tr>
	 		<th>Tài khoản</th>
	    	<th>Thời gian</th>
	        <th>Thay đổi</th>
	        <th>Admin thực hiện</th>
	 	</tr>
	</thead>
	<tbody>
		<?php if(count($view_data['model']) > 0) { $sum = 0; foreach($view_data['model'] as $item) { ?> 
	 	<tr>
	 		<td><?= $item[1] ?></td>
	        <td>
	        	<?= date_format($item[4], 'd/m/Y h:i:s A') ?>	
	    	</td>
	        <td>
	        	<?php if($item[2] == "Tăng lên") {  $sum += $item[3]; ?>
	        		<label class="label label-success" style="font-size:12px"><i class="fa fa-arrow-up"></i> <?= $item[2] ." ".str_replace(',','.',number_format($item[3])). " xu" ?></label>
	        	<?php } else if ($item[2] == "Giảm xuống") {  $sum -= $item[3]; ?>
					<label class="label label-danger" style="font-size:12px"><i class="fa fa-arrow-down"></i> <?= $item[2] ." ". str_replace(',','.',number_format($item[3])). " xu" ?></label>
	        	<?php } ?>
	        </td>
	        <td><?= $item[6] ?></td>
	 	</tr>
		<?php  } ?> 
		<tr>
			<td>&nbsp;</td>
			<td class="text-right">Tổng thay đổi:</td>
			<td><label style="font-size:14px" class="label label-primary"><?=  str_replace(',','.',number_format($sum)) ?> xu </label></td>
			<td></td>
		</tr>
		<?php } else {  ?>
		<tr>
			<td colspan="4" class="text-center">
				<h5>Không tìm thấy dữ liệu</h5>
			</td>
		</tr>
		<?php } ?>
	</tbody>
</table>
<!-- DataTables -->
<!-- <script>
        $('#tblData').DataTable({
            order: [[0, 'desc']],
            paging:false,
            ordering: false,
            searching: false,
            lengthChange: true,
            retrieve: true,
        });
</script> -->