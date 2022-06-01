<template>
  <div class="container pt-5">
    <div class="row">
      <div class="col">
        <h3>All Groups</h3>
      </div>
    </div>
    <div class="row">
      <div class="col-4 my-2" v-for="g in groups" :key="g.id">
        <GroupCard :group="g" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState.js'
import { groupsService } from '../services/GroupsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    onMounted(async () => {
      try {
        await groupsService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast('Something Went Wrong', 'error')
      }
    })
    return {
      groups: computed(() => AppState.groups)
    }
  }
}
</script>

<style scoped lang="scss">
</style>
