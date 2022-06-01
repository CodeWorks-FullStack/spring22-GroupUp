<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <h1>Group Page</h1> {{ group }}
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState.js'
import { groupsService } from '../services/GroupsService.js'
import { membersService } from '../services/MembersService.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const route = useRoute()
    const router = useRouter()

    async function joinGroup() {
      try {
        await membersService.joinGroup(route.params.id)
        await groupsService.getById(route.params.id)
      } catch (error) {
        Pop.toast("Its all gone wrong")
        router.push({ name: 'Home' })
      }
    }


    onMounted(async () => {
      try {
        await groupsService.getById(route.params.id)
      } catch (error) {
        if (await Pop.confirm("This group is Private", "Do you wish to join the group?", "question", "Join")) {
          await joinGroup()
        } else {
          router.push({ name: 'Home' })
        }
      }
    })
    return {
      group: computed(() => AppState.activeGroup)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>