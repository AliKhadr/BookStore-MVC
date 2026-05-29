$('#tblData').DataTable({
    ajax: '/product/GetAll',
    columns: [
        { data: 'title' },
        { data: 'isbn' },
        { data: 'price' },
        { data: 'author' },
        { data: 'category.name' },
        { defaultContent: '' }
    ]
});