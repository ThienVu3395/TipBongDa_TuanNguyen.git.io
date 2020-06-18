<script src="<?= base_url?>js/functions.js"></script>
<script>
	$(function() {
		$('#name').keyup(function() {
	        if ($(this).val() != '') {
	            $('#alias').val(generateSlug($(this).val()));
	        }
    	});
	});
</script>