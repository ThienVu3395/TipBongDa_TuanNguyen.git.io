<!-- DataTables -->
<script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
<script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
<script>
    $(function () {
         $("#keyword").val("<?= $_GET['cAccName']; ?>");
         $(document).on("keyup", "#keyword", function(e){
            var self = $(this);
            if(e.keyCode == 13)
            {
                $.post(self.attr("data-urlSubmit"), { keyword: self.val() }, function(result){
                    $("#infoUser").html(result);
                });   
            }   
         });
         $(document).on("click","#btnSearch", function(e){
            var self = $(this);
           $.post(self.attr("data-urlSubmit"), { keyword: $("#keyword").val(), page: self.attr("data-pageCurrent") }, function(result){
                    $("#infoUser").html(result);
            }); 
         });
         $("#btnSearch").click().bind();

    });
</script>