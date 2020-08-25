<template>
    <div class="home" style="text-align:center;">
        <h1>{{ msg }}</h1>
        <slider :bgWidth="bgWidth" :slideWidth="slideWidth" :backgroundImg="backgroundImg" :slideImg="slideImg" :positionX="positionX" :positionY="positionY"></slider>
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
                key: '',
                bgWidth: 0,
                bgHeight: 0,
                slideWidth: 0,
                backgroundImg: '',
                slideImg: '',
                positionX: 0,
                positionY: 0
            }
        },
        created() {
            this.loadData()
        },
        mounted() {
        },
        methods: {
            loadData() {
                getVerification().
                    then(response => {
                        this.key = response.key;
                        this.bgWidth = response.bgWidth;
                        this.bgHeight = response.bgHeight;
                        this.slideWidth = response.slideWidth;
                        this.backgroundImg = response.backgroundImg;
                        this.slideImg = response.slideImg;
                        this.positionX = response.positionX;
                        this.positionY = response.positionY;
                    })
            },
            verifyData() {
                var param = { key: this.key, x: this.positionX, y: this.positionY };
                verify(param)
                    .then(response => {
                        if (response == true) {

                        }
                    });
            }
        }
    };
</script>
