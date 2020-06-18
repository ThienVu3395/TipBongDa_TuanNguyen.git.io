<!-- DataTables -->
<script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
<script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
<script>
    $(function () {
        $('#tblData').DataTable({
            columnDefs: [{ orderable: false, targets: [8] }],
            order: [[0, 'asc']],
            paging:false,
            searching: false,
            lengthChange: true,
            retrieve: true,
        });
         $(document).on("click",".pagination a", function(e){
        	e.preventDefault();
        	var self = $(this);
        	$.post(self.attr("href"), function(result){
        		$("#box").html(result);
        	});
        });
        $.get("<?= base_url_admin?>/user/paging&page=1", function(result){
                $("#box").html(result);
        });
         $(document).on("keyup", "#keyword", function(e){
            var self = $(this);
            if(e.keyCode == 13)
            {
                $.post(self.attr("data-urlSubmit"), { keyword: self.val(), page: self.attr("data-pageCurrent") }, function(result){
                    $("#box").html(result);
                });   
            }   
         });
         $(document).on("click","#btnSearch", function(e){
            var self = $(this);
           $.post(self.attr("data-urlSubmit"), { keyword: $("#keyword").val(), page: self.attr("data-pageCurrent") }, function(result){
                    $("#box").html(result);
            }); 
         });

        
    });
</script>