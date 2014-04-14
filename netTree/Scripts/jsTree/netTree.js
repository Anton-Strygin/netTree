
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

function selectNode(e, data) {
    var treeInstance = $('#' + this.id);    
    var rootNodeId = e.data.rootNodeId;
    var amountOfTreeNodes = e.data.amountOfTreeNodes;
    
    treeInstance.off('select_node.jstree');
    var nodeId = data.node.id;
    if (nodeId == rootNodeId) { 
        treeInstance.jstree(true).select_all();             
        treeInstance.find('a').first().find('i').first().attr('class', 'jstree-icon jstree-checkbox');
    }
    else {
        treeInstance.find('a').first().find('i').first().attr('class', 'jstree-icon jstree-checkbox jstree-undetermined ');
        var checkedItems = treeInstance.jstree(true).get_selected().length;
        var uncheckedItems = amountOfTreeNodes - checkedItems;
        if (uncheckedItems <= 1) {
            treeInstance.jstree(true).select_node('#'+rootNodeId);
            treeInstance.find('a').first().find('i').first().attr('class', 'jstree-icon jstree-checkbox');
        }
    } //else
    treeInstance.on('select_node.jstree', { 'amountOfTreeNodes': amountOfTreeNodes, 'rootNodeId': rootNodeId }, selectNode);

}

function deselectNode(e, data) {
    var treeInstance = $('#' + this.id);
    var rootNodeId = e.data.rootNodeId;
    var amountOfTreeNodes = e.data.amountOfTreeNodes;
    
    treeInstance.off('deselect_node.jstree');
    var nodeId = data.node.id;
    if (nodeId == rootNodeId) { treeInstance.jstree(true).deselect_all(); }
    else {
        var checkedItems = treeInstance.jstree(true).get_selected().length;
        var uncheckedItems = amountOfTreeNodes - checkedItems;

        if (uncheckedItems >= 0 && treeInstance.jstree(true).is_selected('#' + rootNodeId))
        {
            treeInstance.jstree(true).deselect_node('#'+rootNodeId);
            treeInstance.find('a').first().find('i').first().attr('class', 'jstree-icon jstree-checkbox jstree-undetermined');
        }
        if (checkedItems == 0)
        {
            treeInstance.jstree(true).deselect_node('#'+rootNodeId);
            treeInstance.find('a').first().find('i').first().attr('class', 'jstree-icon jstree-checkbox');
        }
    } // else");
    treeInstance.on('deselect_node.jstree', { 'amountOfTreeNodes': amountOfTreeNodes, 'rootNodeId': rootNodeId }, deselectNode);
} // function deselectNode

function setRootNodeState(treeId, rootNodeId, selectedItems, nodesCount) {
    if(selectedItems > 0 && selectedItems  < nodesCount) {
        $('#' + treeId).find('a').first().find('i').first().attr('class', 'jstree-icon jstree-checkbox jstree-undetermined ');
    }
    var uncheckedItems = nodesCount - selectedItems+1;
    if (uncheckedItems <= 1)
    {
        $('#' + treeId).jstree(true).select_node('#' + rootNodeId);
        $('#' + treeId).find('a').first().find('i').first().attr('class', 'jstree-icon jstree-checkbox');
    }
}