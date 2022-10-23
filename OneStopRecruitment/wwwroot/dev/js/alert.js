function showAlert(type, message) {
    if (type === "FAILED") {
        var popUpFailed = `
            <input hidden id="hidden_btnFailed" data-toggle="modal" data-target="#failedMessage">
            <div id="failedMessage" class="modal animate__animated dis-none" data-animate-in="animate__bounceIn" data-animate-out="animate__zoomOut" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="failedMessageCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
                    <div class="modal-content px-3 py-4">
                        <div class="modal-body mt-3">
                            <div class="container">
                                <div class="row align-items-center">
                                    <div class="d-flex flex-row">
                                        <svg id="failedSvg" version="1.1" xmlns="http://www.w3.org/2000/svg" viewBox="-10 -10 150 150">
                                            <circle class="path circle" fill="none" stroke-width="10" stroke-miterlimit="20" cx="65.1" cy="65.1" r="62.1" />
                                            <line class="path line" fill="none" stroke-width="10" stroke-linecap="round" stroke-miterlimit="20" x1="34.4" y1="37.9" x2="95.8" y2="92.3" />
                                            <line class="path line" fill="none" stroke-width="10" stroke-linecap="round" stroke-miterlimit="20" x1="95.8" y1="38" x2="34.4" y2="92.2" />
                                        </svg>
                                        <span class="modal-message-failed ml-4 justify-content-center align-self-center break-word">` + message + `</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="align-content-end">
                                <input id="btnModalClose" type="button" class="C--button type--1 -theme-primary" value="Close" data-dismiss="modal">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;

        $('.site #failedMessage').remove();
        $('.site').append(popUpFailed);
        $('#failedMessage').modalAnimation();
        $("#hidden_btnFailed").trigger("click");
    } else if (type === "SUCCESS") {
        var popUpSuccess = `
            <input hidden id="hidden_btnSuccess" data-toggle="modal" data-target="#successMessage">
            <div id="successMessage" class="modal animate__animated dis-none" data-animate-in="animate__bounceIn" data-animate-out="animate__zoomOut" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="successMessageCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
                    <div class="modal-content px-3 py-4">
                        <div class="modal-body mt-3">
                            <div class="container">
                                <div class="row align-items-center">
                                    <div class="d-flex flex-row">
                                        <svg id="successSvg" version="1.1" xmlns="http://www.w3.org/2000/svg" viewBox="-10 -10 150 150">
                                            <circle class="path circle" fill="none" stroke-width="10" stroke-miterlimit="20" cx="65.1" cy="65.1" r="62.1" />
                                            <polyline class="path check" fill="none" stroke-width="10" stroke-linecap="round" stroke-miterlimit="20" points="100.2,40.2 51.5,88.8 29.8,67.5 " />
                                        </svg>
                                        <span class="modal-message-success ml-4 justify-content-center align-self-center ">` + message + `</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="align-content-end">
                                <input id="btnModalClose" type="button" class="C--button type--1 -theme-primary" value="Close" data-dismiss="modal">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;

        $('.site #successMessage').remove();
        $('.site').append(popUpSuccess);
        $('#successMessage').modalAnimation();
        $("#hidden_btnSuccess").trigger("click");
    } else if (type === "LOADING") {
        var popUpLoader = `
            <input hidden id="hidden_btnLoader" data-toggle="modal" data-target="#loadingMessage">
            <div id="loadingMessage" class="modal animate__animated dis-none" data-animate-in="animate__bounceIn" data-animate-out="animate__zoomOut" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="loadingMessageCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
                    <div class="modal-content px-3 py-5">
                        <div class="modal-body my-3">
                            <div class="container">
                                <div class="row align-items-center">
                                    <div class="d-flex flex-row">
                                        <svg id="loadingSvg" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                            <g transform="translate(90,50)">
                                                <g transform="rotate(0)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="1" transform="scale(1.07182 1.07182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.72s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.72s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(82.36067977499789,73.51141009169893)">
                                                <g transform="rotate(36)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.9" transform="scale(1.12182 1.12182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.64s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.64s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(62.3606797749979,88.04226065180615)">
                                                <g transform="rotate(72)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.8" transform="scale(1.17182 1.17182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.56s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.56s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(37.63932022500211,88.04226065180615)">
                                                <g transform="rotate(108)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.7" transform="scale(1.22182 1.22182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.48s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.48s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(17.63932022500211,73.51141009169893)">
                                                <g transform="rotate(144)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.6" transform="scale(1.27182 1.27182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.4s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.4s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(10,50.00000000000001)">
                                                <g transform="rotate(180)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.5" transform="scale(1.32182 1.32182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.32s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.32s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(17.639320225002102,26.48858990830108)">
                                                <g transform="rotate(216)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.4" transform="scale(1.37182 1.37182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.24s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.24s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(37.639320225002095,11.957739348193861)">
                                                <g transform="rotate(252)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.3" transform="scale(1.42182 1.42182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.16s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.16s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(62.36067977499789,11.957739348193854)">
                                                <g transform="rotate(288)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.2" transform="scale(1.47182 1.47182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="-0.08s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="-0.08s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                            <g transform="translate(82.36067977499789,26.488589908301066)">
                                                <g transform="rotate(324)">
                                                    <circle class="circle" cx="0" cy="0" r="5" fill-opacity="0.1" transform="scale(1.02182 1.02182)">
                                                        <animateTransform attributeName="transform" type="scale" begin="0s" values="1.5 1.5;1 1" keyTimes="0;1" dur="0.8s" repeatCount="indefinite"></animateTransform>
                                                        <animate attributeName="fill-opacity" keyTimes="0;1" dur="0.8s" repeatCount="indefinite" values="1;0" begin="0s"></animate>
                                                    </circle>
                                                </g>
                                            </g>
                                        </svg>
                                        <span class="modal-message-loading ml-4 justify-content-center align-self-center ">` + message + `</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;

        $('.site #loadingMessage').remove();
        $('.site').append(popUpLoader);
        $('#loadingMessage').modalAnimation();
        $("#hidden_btnLoader").trigger("click");
    }
}