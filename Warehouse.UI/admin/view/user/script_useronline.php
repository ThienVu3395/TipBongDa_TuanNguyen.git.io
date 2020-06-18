<!-- DataTables -->
<script src="bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
<script src="bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
<script>
    $(function () {
        $('#tblData').DataTable({
            columnDefs: [{ orderable: false, targets: [8] }],
            order: [[0, 'asc']],
            paging:true,
            searching: true,
            lengthChange: true,
            retrieve: true,
        });
        
    });
</script>