
function hideNetTree(treeId) {
    $('#' + treeId).hide();
}

function showNetTree(treeId) {
    $('#' + treeId).show();
}

function rememberCheckedNodes(treeId, selectedItemsId) {
    var selectedIds = $('#' + treeId).jstree(true).get_selected();
    document.getElementById(selectedItemsId).value = selectedIds.join(',');
    return true;
}