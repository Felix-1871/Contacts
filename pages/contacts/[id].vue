<template>
    <div>
        <Head>
        <Title>{{ contact.firstName }}</Title>
        </Head>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h1 class="display-4">{{ contact.firstName }}</h1>
                </div>
            </div>
        </div>
    </div>
                    


</template>

<script>

export default{
    data(){
        return{
            contact: []
        }
    },
        async mounted(){
            const response = await $fetch('https://localhost:7160/api/Contact/' + this.$route.params.id)
            this.contact = response
            console.log(this.contact)

        }
}

</script>