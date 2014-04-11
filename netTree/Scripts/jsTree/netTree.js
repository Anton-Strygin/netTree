
function hideNetTree(treeId) {
    $('#' + treeId).hide();
}

function showNetTree(treeId) {
    $('#' + treeId).show();
}

function rememberCheckedNodes(treeId, selectedItemsId, validateSelectedItems) {
    var selectedIds = $('#' + treeId).jstree(true).get_selected();
    document.getElementById(selectedItemsId).value = selectedIds.join(',');
    if (validateSelectedItems && (typeof validateSelectedItems == "function")) {
        return validateSelectedItems(selectedIds);
    }
    return true;
}