/*var ViewModel = function () {
    var self = this;
    self.pieces = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();

    self.getPieceDetail = function (item) {
        ajaxHelper(piecesUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    };

    var piecesUri = '/api/pieceofarts/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllPieces() {
        ajaxHelper(piecesUri, 'GET').done(function (data) {
            self.pieces(data);
        });
    }

    // Fetch the initial data.
    getAllPieces();
};

ko.applyBindings(new ViewModel());*/