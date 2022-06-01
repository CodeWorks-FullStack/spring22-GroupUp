<template>
  <router-link :to="{ name: 'Group', params: { id: group.id } }">
    <div class="group d-flex align-items-end p-3 justify-content-between selectable">
      <h5 class="text-white">{{ group.name }}</h5>
      <img height="35" class="rounded-pill" :src="group.creator.picture" alt="">
    </div>
  </router-link>
</template>


<script>
import { computed } from 'vue'

export default {
  props: {
    group: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      imgUrl: computed(() => ` linear-gradient(to top, rgba(50, 50, 50, 0.60) 0% 1%, transparent 20% 100%), url(${props.group.image})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.group {
  width: 100%;
  height: 200px;
  background-image: v-bind(imgUrl);
  background-size: cover;
  transition: all .3s ease;

  &:hover {
    transform: scale(1.02);
  }
}


h5 {
  text-shadow: 1px 1px 3px rgb(50, 50, 50);
}
</style>