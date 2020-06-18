
<table id="tblData" class="table table-bordered" >
	<thead>
        <tr>
            <th>Tài khoản</th>
            <th>Thời gian</th>
            <th>Giá trị cũ</th>
	        <th>Giá trị mới</th>
	        <th>Admin thực hiện</th>
        </tr>
	</thead>
	<tbody>
		<?php if(count($view_data['model']) > 0) { foreach($view_data['model'] as $item) { ?> 
	 	<tr>
            <td>
                <?= $item['cAccName'] ?>
            </td>
            <td>
                <?= date_format($item['dateChange'], 'd/m/Y h:i:s A') ?>	
            </td>
            <td>
                <label class="text text-danger"><?= $item['oldValue'] ?></label>
            </td>
            <td>
                <label class="text text-success"><?= $item['newValue'] ?></label>
            </td>
            <td>
                <?= $item['userChange'] ?>
            </td>
	 	</tr>
		<?php  } ?> 
		
		<?php } else {  ?>
		<tr>
			<td colspan="5" class="text-center">
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