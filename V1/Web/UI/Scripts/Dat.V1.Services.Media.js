Dat.V1.Services.Meida = {
    Asset: "media",
    Service: "file_manager",
    Uploader: {
        EndPoint: "upload",
        Chunk: function (file, chunked) {
            var reader = new FileReader();
            reader.onload = function (evt) {
                var content = evt.target.result;
                var fileSize = content.length;
                var chunks = new Array();
                var index = 0;
                var total = Math.ceil(fileSize / Dat.V1.Config.FileChunkSize);
                for (var i = 0; i < fileSize; i += Dat.V1.Config.FileChunkSize)
                    (function (fil, start, index) {
                        chunks.push(
                            {
                                data: content.slice(start, Dat.V1.Config.FileChunkSize + start),
                                size: Dat.V1.Config.FileChunkSize,
                                index: index,
                                total: total
                            });
                    })(file, i, index++);
                if (chunks.length == total)
                    chunked(chunks);
            };
            reader.readAsDataURL(file);
        },
        Upload: function (file, success, error) {
            Dat.V1.Services.Media.Uploader.Chunk(file,
                    function (chunks) {
                        var upload = function (request_id, chunk) {
                            Dat.V1.Services.Send({
                                Asset: Dat.V1.Services.Media.Asset,
                                Service: Dat.V1.Services.Media.Service,
                                EndPoint: Dat.V1.Services.Media.Uploader.EndPoint,
                                Data: {
                                    manifest: {
                                        info: {
                                            name: escape(file.name),
                                            size: file.size
                                        },
                                        chunk: chunk,
                                        request_id: request_id
                                    }
                                },
                                OnSuccess: function (response) {
                                    success(response);
                                    if (chunks.length == 0)
                                        return;
                                    upload(response.manifest.request_id, chunks.pop());
                                },
                                OnError: function (state, e) {
                                    error(e);
                                }
                            });
                        }
                        upload(null, chunks.reverse().pop());
                    });
        }
    }
};