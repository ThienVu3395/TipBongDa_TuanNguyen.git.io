 <script src="Scripts/jquery.unobtrusive-ajax.min.js"></script>
<!-- DataTables -->
<script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
<script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
<script>
    $(function () {
        $('#tblData').DataTable({
            columnDefs: [{ orderable: false, targets: [7] }],
            lengthChange: false,
            searching: false
        });
       
    });
</script>