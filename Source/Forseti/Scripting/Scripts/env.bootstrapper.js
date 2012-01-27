function executeSpecs() {
    try {
        Envjs.log = function (string) {
            return;
        };

        Envjs({
            scriptTypes: {
                '': true,
                'text/javascript': true,
                'text/envjs': false
            }
        });

        var self = this;

        window.onload = function () {
            //print("DONE");
        }

        window.location = window.pagePath; 
    } catch( exception ) {
        print("EXCEPTION : "+exception);
    }
}