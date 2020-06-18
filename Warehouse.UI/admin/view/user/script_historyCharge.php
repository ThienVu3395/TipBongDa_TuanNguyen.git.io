<!-- DataTables -->
<script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
<script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>   
<script>
    $(function () {
         $(document).on("click",".pagination a", function(e){
            e.preventDefault();
            var self = $(this);
            $.post(self.attr("href"), function(result){
                $("#box").html(result);
            });
        });
        $.get("<?=base_url_admin?>/user/pagingHistoryCharge&page=1&cAccName=<?= $view_data['cAccName'] ?>",function(result){
			$("#box").html(result);
        });
    });
</script>