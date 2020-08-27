<template>
    <div class="home" style="text-align:center;">
        <h1>{{ msg }}</h1>
        <slider :loadDataFunc="loadData" :verifyFunc="verifyData"></slider>
    </div>
</template>

<script>
    import Slider from "@/slider/slider.vue";
    import { getVerification, verify } from "@/api/VerificationDemoApi"

    export default {
        name: 'Home',
        components: { Slider },
        props: {
            msg: String
        },
        data() {
            return {
                key: ''
            }
        },
        created() {
        },
        methods: {
            //加载数据，这个函数要slider组件回调
            loadData(callback) {
                getVerification().
                    then(response => {
                        this.key = response.key;
                        if (callback) {
                            callback(response);
                        }
                    })
            },
            //校验结果，这个函数要slider组件回调
            verifyData(param, callback) {
                verify(param)
                    .then(response => {
                        if (callback) {
                            callback(response);
                        }
                        if (response == true) {
                            window.setTimeout(function () {
                                //添加后续处理逻辑
                            }, 2000);
                        }
                    });
            }
        }
    };
</script>
