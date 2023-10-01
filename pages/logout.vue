<script>
export default {
    async mounted() {
        //delete the cookie
        document.cookie = "loggedIn=true;max-age=0"
        this.$router.push('/')
    }
}

</script>

<template>
    <div class="login-page">

        <Head>
            <Title>Logout</Title>
        </Head>
        <h1>Logout</h1>
        <p>You have been logged out</p>
    </div>
</template>