<template>
  <div>

    <Head>
      <Title>Contacts</Title>
    </Head>
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <h1 class="display-4">Contacts</h1>
          <p class="lead">Simple app to manage contacts</p>
          <div class="nav-links">
            <NuxtLink to="/contacts/add" class="btn btn-primary">Add Contact</NuxtLink>
            <NuxtLink to="/login" class="btn btn-primary">Login</NuxtLink>
            <NuxtLink to="/logout" class="btn btn-primary">Logout</NuxtLink>
          </div>
          <hr />
          <div class="card" v-for="contact in contacts">
            <h4>{{ contact.firstName }}</h4>
            <p>{{ contact.lastName }}</p>
            <p>{{ contact.email }}</p>
            <div class="buttons">
              <NuxtLink :to="`/contacts/${contact.contactId}`" class="btn btn-primary">Show more</NuxtLink>
              <NuxtLink :to="`/contacts/edit/${contact.contactId}`" class="btn btn-primary">Edit</NuxtLink>
              <button class="btn btn-danger">Delete</button>
            </div>
          </div>

        </div>
      </div>
    </div>
  </div>
</template>
  
<script>
export default {
  data() {
    return {
      contacts: []
    }
  },
  async mounted() {
    const response = await $fetch('https://localhost:7160/api/contact')
    this.contacts = response
  }
}
</script>
  
<style scoped>
.display-4 {
  margin-bottom: 40px;
}

.nav-links {
  margin-bottom: 20px;
}

.nav-links a {
  margin-right: 10px;
}

.nav-links a:last-child {
  margin-right: 0;
}


.card {
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.buttons {
  display: flex;
  justify-content: space-between;
}

.card {
  margin-bottom: 20px;
}

.card h4 {
  margin-bottom: 0;
}

.card p {
  margin-top: 0;
}

.card p:last-child {
  margin-bottom: 0;
}

.card .buttons {
  margin-top: 20px;
}

.card .buttons button {
  margin-right: 10px;
}

.card .buttons button:last-child {
  margin-right: 0;
}

.card .buttons a {
  margin-right: 10px;
}

.card .buttons a:last-child {
  margin-right: 0;
}
</style>