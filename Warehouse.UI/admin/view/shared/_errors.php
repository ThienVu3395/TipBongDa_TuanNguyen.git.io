<?php if(isset($view_data['errors']) && count($view_data['errors']) > 0 ) { ?>   
<ul style='color:#c80209'>
	<?php foreach ($view_data['errors'] as $error) { ?>
		<li><?= $error ?></li>
	<?php } ?>
</ul>
<?php } ?>