<script setup lang="ts">
import { useRouter, type RouteRecordNormalized } from 'vue-router'

const routes: RouteRecordNormalized[] = useRouter().getRoutes()
</script>
<template>
  <Card>
    <template #content>
      <Fieldset legend="ИС 'Складской учет'">
        <template #default>
          <div style="display: grid; grid-template-columns: 1fr 1fr 1fr; gap: 10px">
            <div v-for="(route, index) in routes" :key="index">
              <Button
                fluid
                v-if="route.path != '/'"
                :label="route.name?.toString()"
                as="router-link"
                :to="{ path: route.path }"
              ></Button>
            </div>
          </div>
        </template>
      </Fieldset>
    </template>
  </Card>
</template>
