<!-- DataTables -->
<script src="<?= base_url ?>/admin/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
<script src="<?= base_url ?>/admin/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
<!-- date-range-picker -->
<script src="<?= base_url ?>/admin/bower_components/moment/min/moment.min.js"></script>
<script src="<?= base_url ?>/admin/bower_components/bootstrap-daterangepicker/daterangepicker.js"></script>
<script>
     $(function () {
        //Date range picker with time picker
         $('#reservation').daterangepicker({locale: { format: 'DD/MM/YYYY', cancelLabel: 'Clear' }});
         // $('#reservation').val("");
         $(document).on("submit","#frmSearch", function(e){
            e.preventDefault();
            $.ajax({
                url: $(this).attr("action"),
                method: $(this).attr("method"),
                data: $(this).serialize(),
                success: function(result){
                    $("#divTableData").html(result);
                }
            });
         });
         $("#frmSearch").submit();
     
    });
</script>